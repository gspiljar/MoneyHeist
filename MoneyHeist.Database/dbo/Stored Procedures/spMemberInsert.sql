CREATE PROCEDURE [dbo].[spMemberInsert]
(
    @Id INT OUTPUT,
    @Name NVARCHAR(100),
    @Sex CHAR(1),
    @Email NVARCHAR(100),
    @MainSkill NVARCHAR(100),
    @Status VARCHAR(12),
    @Skills dbo.SkillInsertType READONLY
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO [dbo].[Members] ([Name], [Sex], [Email], [MainSkill], [Status])
        VALUES (@Name, @Sex, @Email, @MainSkill, @Status);

        SET @Id = SCOPE_IDENTITY();

        INSERT INTO [dbo].[Skills] ([Name], [Level])
        SELECT T.[Name], T.[Level]
        FROM @Skills AS T;

        INSERT INTO [dbo].[Members_Skills] ([MemberId], [SkillId])
        SELECT @Id, S.[Id]
        FROM @Skills AS T
        INNER JOIN [dbo].[Skills] AS S ON S.[Name] = T.[Name] AND S.[Level] = T.[Level];

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRAN ImportCSVData
		END;
		THROW
	END CATCH
END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[spMemberInsert] TO [WebUsers]
    AS [dbo];
GO