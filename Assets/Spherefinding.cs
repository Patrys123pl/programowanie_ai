using UnityEngine;

public class Spherefinding : MonoBehaviour
{
    public Transform target; // Cel, do którego sfera ma dotrzeæ
    public float speed = 5f; // Prêdkoœæ ruchu sfery

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
        // Jeœli istnieje œcie¿ka i nie osi¹gniêto jeszcze celu
        if (pathfinding != null && pathfinding.path != null && pathfinding.targetIndex < pathfinding.path.Count)
        {
            // Oblicz kierunek ruchu
            Vector3 direction = (pathfinding.path[pathfinding.targetIndex] - transform.position).normalized;

            // Przesuñ sferê w kierunku kolejnego punktu na œcie¿ce
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Jeœli sfera jest wystarczaj¹co blisko aktualnego punktu na œcie¿ce, przejdŸ do nastêpnego punktu
            if (Vector3.Distance(transform.position, pathfinding.path[pathfinding.targetIndex]) < 0.2f)
            {
                pathfinding.targetIndex++;
            }
        }
    }

    private Vector3 GetRandomTargetPosition()
    {
        // Tutaj dodaj logikê do generowania losowej pozycji docelowej
        // Na przyk³ad, mo¿esz wylosowaæ wspó³rzêdne w obrêbie labiryntu
        // i zwróciæ je jako Vector3
        return Vector3.zero;
    }
}