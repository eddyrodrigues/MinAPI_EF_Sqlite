namespace ViewModels
{
  public class ResultViewModel<T>
  {
    public T Data { get; private set; }
    public List<string> Errors { get; private set; } = new List<string>();
    public ResultViewModel(T Data, List<string> errors)
    {
      this.Data = Data;
      this.Errors = errors;
    }
    public ResultViewModel(T Data)
    {
      this.Data = Data ;
    }

    public ResultViewModel(List<string> Errors)
    {
      this.Errors = Errors;
    }

    public ResultViewModel(string Error)
    {
      this.Errors.Add(Error);
    }
  }
}
