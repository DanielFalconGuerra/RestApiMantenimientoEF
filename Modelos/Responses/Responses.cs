namespace RestApiMantenimientoEF.Modelos.Responses
{
    public class AccionResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}