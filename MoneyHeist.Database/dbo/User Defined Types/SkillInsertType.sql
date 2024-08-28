CREATE TYPE [dbo].[SkillInsertType] AS TABLE
(
	[Name] [nvarchar](100),
	[Level] [char](10)
)

GO
GRANT CONTROL
    ON TYPE::[dbo].[SkillInsertType] TO [WebUsers];