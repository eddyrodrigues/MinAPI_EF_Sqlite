namespace ViewModel{
    public class ResultViewModel<T> {
        public T Data { get; set; }
        public List<string> errors { get; set; }
        
        public ResultViewModel(T data)
        {
            this.Data = data;
        }
        public ResultViewModel(List<string> erros)
        {
            this.errors = erros;
        }
        public ResultViewModel(string Error)
        {
            this.errors = new List<string> () { Error };
        }
    }
}