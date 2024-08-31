using UnityEngine;

public class Spherefinding : MonoBehaviour
{
    public Transform target; // Cel, do kt�rego sfera ma dotrze�
    public float speed = 5f; // Pr�dko�� ruchu sfery

    private Pathfinding pathfinding;
    private Vector3 targetPosition = Vector3.zero;

    void Start()
    {
        pathfinding = new Pathfinding();
        targetPosition = GetRandomTargetPosition();
        pathfinding.target = targetPosition;
        pathfinding.GeneratePath();
    }

    void Update()
    {
        // Je�li istnieje �cie�ka i nie osi�gni�to jeszcze celu
        if (pathfinding != null && pathfinding.path != null && pathfinding.targetIndex < pathfinding.path.Count)
        {
            // Oblicz kierunek ruchu
            Vector3 direction = (pathfinding.path[pathfinding.targetIndex] - transform.position).normalized;

            // Przesu� sfer� w kierunku kolejnego punktu na �cie�ce
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Je�li sfera jest wystarczaj�co blisko aktualnego punktu na �cie�ce, przejd� do nast�pnego punktu
            if (Vector3.Distance(transform.position, pathfinding.path[pathfinding.targetIndex]) < 0.2f)
            {
                pathfinding.targetIndex++;
            }
        }
    }

    private Vector3 GetRandomTargetPosition()
    {
        // Tutaj dodaj logik� do generowania losowej pozycji docelowej
        // Na przyk�ad, mo�esz wylosowa� wsp�rz�dne w obr�bie labiryntu
        // i zwr�ci� je jako Vector3
        return Vector3.zero;
    }
}