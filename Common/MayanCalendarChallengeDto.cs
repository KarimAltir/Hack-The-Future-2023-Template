namespace Common;

public record MayanCalendarChallengeDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Day { get; set; }
}