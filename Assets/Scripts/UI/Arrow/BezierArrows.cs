using System.Collections.Generic;
using UnityEngine;

namespace UI.Arrow
{
    public class BezierArrows : MonoBehaviour
    {
        public GameObject ArrowHeadPrefab;
        public GameObject ArrowNodePrefab;
        public int NodeCount;
        public float NodeScale;

        private readonly List<RectTransform> _nodes = new();

        private readonly List<Vector2> _controlPointsFactors = new() 
            { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };
        
        private readonly List<Vector2> _controlPoints = new();
        //private RectTransform _origin;
        private Vector2 _origin;
        private bool _enabled;


        private void Awake()
        {
            //_origin = GetComponent<RectTransform>();

            for (int i = 0; i < NodeCount; i++)
            {
                var node = Instantiate(ArrowNodePrefab, transform).GetComponent<RectTransform>();
                _nodes.Add(node);
            }
            
            var head = Instantiate(ArrowHeadPrefab, transform).GetComponent<RectTransform>();
            _nodes.Add(head);

            foreach (var node in _nodes)
            {
                node.position = new Vector3(-1000, -1000);
            }

            for (int i = 0; i < 4; i++)
            {
                _controlPoints.Add(Vector2.zero);
            }
        }

        public void Enabled(bool value)
        {
            _origin = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _enabled = value;

            foreach (var node in _nodes)
            {
                node.gameObject.SetActive(value);
            }
        }

        public void SetHeadColor(Color color)
        {
            
        }

        private void Update()
        {
            if (!_enabled)
                return;
            
            var originPosition = _origin;
            _controlPoints[0] = new Vector2(originPosition.x, originPosition.y);

            _controlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            _controlPoints[1] = _controlPoints[0] + (_controlPoints[3] - _controlPoints[0]) * _controlPointsFactors[0];
            _controlPoints[2] = _controlPoints[0] + (_controlPoints[3] - _controlPoints[0]) * _controlPointsFactors[1];

            for (int i = 0; i < _nodes.Count; i++)
            {
                var t = Mathf.Log(1f * i / (_nodes.Count - 1) + 1f, 2f);

                _nodes[i].position = Mathf.Pow(1 - t, 3) * _controlPoints[0] +
                                     3 * Mathf.Pow(1 - t, 2) * t * _controlPoints[1] +
                                     3 * (1 - t) * Mathf.Pow(t, 2) * _controlPoints[2] +
                                     Mathf.Pow(t, 3) * _controlPoints[3];

                if (i > 0)
                {
                    var euler = new Vector3(0f, 0f,
                        Vector2.SignedAngle(Vector2.up, _nodes[i].position - _nodes[i - 1].position));

                    _nodes[i].rotation = Quaternion.Euler(euler);
                }
                
                var scale = NodeScale * (1f -0.03f * (NodeCount - 1 - i));
                _nodes[i].localScale = new Vector3(scale, scale, 1f);
            }

            _nodes[0].transform.rotation = _nodes[1].transform.rotation;
        }
    }
}