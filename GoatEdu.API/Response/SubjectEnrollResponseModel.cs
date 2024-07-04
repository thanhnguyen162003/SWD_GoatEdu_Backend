
namespace GoatEdu.API.Response;

public class SubjectEnrollResponseModel
{
    public IEnumerable<SubjectResponseModel> SubjectEnrollment { get; set;}
    public int? NumberOfSubjectEnroll { get; set; }

}