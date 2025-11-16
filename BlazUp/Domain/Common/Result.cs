namespace Domain.Common;

//Clases para transportar informacion de los resultados de las operaciones entre capas o misma capa
public class Result {
    //------------------------INITIALIZATION------------------------
    public bool Success { get; private set; }
    public string Description { get; private set; }
    public string? ErrorMessage { get; private set; }
    protected Result(bool success, string description, string? errorMsg =  null) {
        Success = success;
        Description = description;
        ErrorMessage = errorMsg;
    }

    //------------------------STATIC METHODS------------------------
    public static Result Ok(string description) => new (true, description);
    public static Result Fail(string description, string? errorMessage = null) 
        => new (false, description, errorMessage);
}

//------------------------GENERIC------------------------
public class Result<T> : Result {
    //------------------------INITIALIZATION------------------------
    public T? Value { get; private set; }
    private Result(bool success, string description, T? value, string? errorMsg =  null) : 
        base(success, description, errorMsg) 
    {
        Value = value;
    }

    //------------------------STATIC METHODS------------------------
    public static Result<T> Ok(string description, T value) => new (true, description, value);
    public static new Result<T> Fail(string description, string? errorMessage = null)
        => new(false, description, default, errorMessage);
}