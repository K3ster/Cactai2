using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Ustawienia ruchu")]
    public float speed = 2f; // Pr�dko�� poruszania si�
    private bool movingLeft = true; // Domy�lnie idziemy w lewo

    [Header("Detekcja kraw�dzi")]
    // Punkt, z kt�rego wystrzeliwujemy raycast � najlepiej umie�ci� jako child obiektu przeciwnika
    public Transform groundDetection;
    // Maksymalna odleg�o�� sprawdzania pod�o�a
    public float detectionDistance = 2f;
    // Warstwa, na kt�rej znajduje si� pod�o�e
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

        // Raycast sprawdzaj�cy, czy przed przeciwnikiem (pod okre�lonym punktem) jest pod�o�e
        RaycastHit hit;
        if (!Physics.Raycast(groundDetection.position, Vector3.down, out hit, detectionDistance, groundLayer))
        {
            // Je�li raycast nic nie wykry�, oznacza to, �e osi�gn�li�my kraw�d� platformy � zmie� kierunek.
            Flip();
        }
    }

    // Metoda zmieniaj�ca kierunek ruchu przeciwnika
    void Flip()
    {
        // Odwracamy warto�� logiczn� kierunku
        movingLeft = !movingLeft;

        // Opcjonalnie obracamy przeciwnika, zmieniaj�c skal� X (przydatne, gdy u�ywamy sprite'�w lub chcemy zwizualizowa� zmian� kierunku)
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
