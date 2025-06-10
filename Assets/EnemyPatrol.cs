using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Ustawienia ruchu")]
    public float speed = 2f; // Prêdkoœæ poruszania siê
    private bool movingLeft = true; // Domyœlnie idziemy w lewo

    [Header("Detekcja krawêdzi")]
    // Punkt, z którego wystrzeliwujemy raycast – najlepiej umieœciæ jako child obiektu przeciwnika
    public Transform groundDetection;
    // Maksymalna odleg³oœæ sprawdzania pod³o¿a
    public float detectionDistance = 2f;
    // Warstwa, na której znajduje siê pod³o¿e
    public LayerMask groundLayer;

    void Update()
    {
        // Ruch przeciwnika
        if (movingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        // Raycast sprawdzaj¹cy, czy przed przeciwnikiem (pod okreœlonym punktem) jest pod³o¿e
        RaycastHit hit;
        if (!Physics.Raycast(groundDetection.position, Vector3.down, out hit, detectionDistance, groundLayer))
        {
            // Jeœli raycast nic nie wykry³, oznacza to, ¿e osi¹gnêliœmy krawêdŸ platformy – zmieñ kierunek.
            Flip();
        }
    }

    // Metoda zmieniaj¹ca kierunek ruchu przeciwnika
    void Flip()
    {
        // Odwracamy wartoœæ logiczn¹ kierunku
        movingLeft = !movingLeft;

        // Opcjonalnie obracamy przeciwnika, zmieniaj¹c skalê X (przydatne, gdy u¿ywamy sprite'ów lub chcemy zwizualizowaæ zmianê kierunku)
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
