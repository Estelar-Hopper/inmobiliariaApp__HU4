namespace inmobiliariaApp.Application.Dtos;

public class UploadFileDto
{
    public string FileName { get; set; }
    public Stream FileStream { get; set; }
}