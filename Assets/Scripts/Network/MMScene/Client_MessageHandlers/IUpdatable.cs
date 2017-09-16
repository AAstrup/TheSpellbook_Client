internal interface IUpdatable
{
    void Update(float deltaTime);
    bool HasExpired();
    void End();
}