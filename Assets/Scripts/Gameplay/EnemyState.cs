[System.Serializable]
public class EnemyState {
    public float life = 100.0f;
	public float attack = 10.0f;

    public bool hurt(float damage) {
		life -= damage;
		return life < 0;
	}
}