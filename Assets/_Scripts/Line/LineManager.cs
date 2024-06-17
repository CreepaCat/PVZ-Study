using UnityEngine;

namespace PVZ.Lines
{
    public class LineManager : MonoBehaviour
    {

        [SerializeField] Grid placementGrid = null;
        [SerializeField] BoxCollider2D linePrefab = null;

        [SerializeField] int rawNum = 5;
        [SerializeField] int columNum = 9;
        void Start()
        {
            InitialLines();
        }

        private void InitialLines()
        {
            float width = columNum * placementGrid.cellSize.x;
            float height = placementGrid.cellSize.y;
            float yOffset = placementGrid.cellSize.y;

            float yStartPos = rawNum / 2 * placementGrid.cellSize.y;
            for (var i = 0; i < rawNum; i++)
            {
                BoxCollider2D lineCollider = Instantiate(linePrefab, transform);
                lineCollider.transform.localScale = new Vector3(width, height, 0);
                //从上往下算第一行
                lineCollider.transform.position = new Vector3(0, yStartPos - yOffset * i, 0);
            }
            //将父物体的中心放在grid格子的中心（奇数格）
            transform.position = placementGrid.transform.position + placementGrid.cellSize * 0.5f;
        }


    }
}


