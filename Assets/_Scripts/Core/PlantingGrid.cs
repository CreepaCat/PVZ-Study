using UnityEngine;

namespace Core
{
    public class PlantingGrid : MonoBehaviour
    {
        private Grid grid;
        public BoxCollider boxCollider;
        void Start()
        {
            grid = GetComponent<Grid>();
            boxCollider = GetComponent<BoxCollider>();
        }

        //绘制网格辅助线
        private void OnDrawGizmos()
        {
            //绘制网格边界线
            Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 1));
            if (grid == null) return;

            Gizmos.color = new Color(0, 0, 1, 1);

            //绘制网格

            //获得网格的左下角的坐标
            Vector3 startPoint = transform.position + boxCollider.center - boxCollider.size.x / 2 * Vector3.right - boxCollider.size.y / 2 * Vector3.up;
            Vector3 center = startPoint + grid.cellSize / 2;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Vector3 newCenterPos = center + new Vector3(j * grid.cellSize.x, i * grid.cellSize.y, 0);
                    // Debug.Log("绘制方格,中心点：" + newCenterPos);
                    //绘制方格
                    Gizmos.DrawCube(newCenterPos, grid.cellSize * 0.9f);
                }
            }

            Gizmos.color = new Color(1, 0, 1, 1);
            Gizmos.DrawCube(Vector3.zero, grid.cellSize * 0.9f);

        }
    }
}


