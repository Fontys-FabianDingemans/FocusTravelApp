namespace FocusTravelApp.Models;

public class ScoredPoint
{
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Activity { get; set; }
    public int Points { get; set; }
    public DateTime CompletedAt { get; set; }
    
    /** Find the ScoredPoint by id */
    public ScoredPoint(int id)
    {
        this.Id = id;
        
        // TODO: Get the ScoredPoint from the database
        this.UserId = 0;
        this.Activity = "Default Activity";
        this.Points = 453;
        this.CompletedAt = DateTime.Now;
    }
    
    public ScoredPoint(int userId, string activity, int points, DateTime completedAt)
    {
        this.UserId = userId;
        this.Activity = activity;
        this.Points = points;
        this.CompletedAt = completedAt;
    }
    
    public ScoredPoint(User user, string activity, int points, DateTime completedAt)
    {
        this.UserId = user.Id;
        this.Activity = activity;
        this.Points = points;
        this.CompletedAt = completedAt;
    }

    public User GetUser()
    {
        return new User(this.UserId);
    }

    public bool IsCompleted()
    {
        return this.CompletedAt != null && this.CompletedAt < DateTime.Now; 
    }
    
}