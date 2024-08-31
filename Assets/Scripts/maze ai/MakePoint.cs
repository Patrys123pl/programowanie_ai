using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePoint : MonoBehaviour
{
    public int[] countH;
    public int[] countW;

    public string X0 = MazeGenerator.Instance.MazeString;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GeneratorPoints();
        }
    }

    public void GeneratorPoints()
    {
        int i = MazeGenerator.Instance.height - 1, j = 0, k = 0, l = 0;

        //   for (int i = MazeGenerator.Instance.height - 1; i > 0; i--)
        //   {
        //      for (int j = 0; j != ' '; j++)
        //      {

        //       }
        //    }

        foreach (var item in MazeGenerator.Instance.MazeString)
        {
            if (item == 0)
            {
                countH[k] = i; k++;
                countW[l] = j; l++;
            }

        }
    }
}