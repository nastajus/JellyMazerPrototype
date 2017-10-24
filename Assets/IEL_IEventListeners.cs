public interface IEL_IEventListeners {

    void OnEnable();

    //OnDisable is necessary to manage properly memory and thus avoid memory leaks
    void OnDisable();
}
