using System.Text;
namespace Shedule.Models;

public class Group
{
    public Guid Id { get; set; } = new Guid();

    public string Course { get; set; }
	public string SpecDesc { get; set; }
	public string SubGroup { get; set; }
	public string TrainDir { get; set; }

	public string CreateGroupCode()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(SpecDesc);
		sb.Append("(");
		sb.Append(TrainDir);
		sb.Append(")-");
		sb.Append(Course);
		sb.Append(SubGroup);
		return sb.ToString();
	}
}