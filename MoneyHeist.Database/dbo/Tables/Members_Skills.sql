CREATE TABLE [dbo].[Members_Skills]
(
	[MemberId] INT NOT NULL,
	[SkillId] INT NOT NULL, 
    CONSTRAINT [FK_Members_Skills_Members] FOREIGN KEY ([MemberId]) REFERENCES [Members]([Id]),
	CONSTRAINT [FK_Members_Skills_Skills] FOREIGN KEY ([SkillId]) REFERENCES [Skills]([Id])
)
