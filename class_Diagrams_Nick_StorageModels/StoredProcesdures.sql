print '' print '*** creating sp_create_award ***'
GO
CREATE PROCEDURE [dbo].[sp_create_award]
	(
	@AwardID	[int],
	@AwardName	[nvarchar](50)
	@AwardDescription [nvarchar](500)
	)
AS
	BEGIN
		INSERT INTO Award
			([AwardID], [AwardName],[AwardDescription])
		  VALUES
			(@AwardID, @AwardName, @AwardDescription)
		RETURN @@ROWCOUNT
	END
GO
print '' print '*** creating sp_select_award ***'
GO
CREATE PROCEDURE [dbo].[sp_select_award]
	(
	@AwardID	[int],
	@AwardName	[nvarchar](50)
	)
AS
	BEGIN
		SELECT AwardID
		FROM Award
		WHERE AwardID = @AwardID
		ORDER BY AwardID ASC
	END
GO

print '' print '*** creating sp_select_all_awards ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_award]
	(
	@AwardID	[int],
	@AwardName	[nvarchar](50)
	)
AS
	BEGIN
		SELECT AwardID
		FROM Award
		ORDER BY AwardID ASC
	END
GO

print '' print '*** creating sp_safely_deactivate_award ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_deactivate_award]
	(
	@AwardID	[int],
	@AwardName	[nvarchar](50)
	)
AS
	BEGIN
		DELETE FROM Award
		WHERE Award = @Award
			AND AwardName = @AwardName
				RETURN @@ROWCOUNT
	END
GO