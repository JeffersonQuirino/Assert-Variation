namespace AssetVariation.Infra.Dto
{
    public class ResponseDto
    {
        public bool Result { get; set; }
        public string Message { get; set; }       
    }

    public class ResponseDto<TResult> : ResponseDto
    {
        public TResult Content { get; set; }
    }
}
