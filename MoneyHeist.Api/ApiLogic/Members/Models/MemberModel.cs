namespace MoneyHeist.Api.ApiLogic.Members.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public string? Email { get; set; }
        public List<SkillModel>? Skills { get; set; }
        public string? MainSkill { get; set; }
        public string? Status { get; set; }
    }
}
