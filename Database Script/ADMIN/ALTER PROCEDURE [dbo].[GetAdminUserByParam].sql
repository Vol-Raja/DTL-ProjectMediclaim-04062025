USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetAdminUserByParam]    Script Date: 06-12-2022 6.55.14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetAdminUserByParam]
(	
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@AdminUserId INT
)
As
BEGIN
	SELECT
		AdminUserId
		,[Name]
		,EmailAddress
		,PhoneNumber
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
		,Username
	FROM dbo.AdminUser
	WHERE 
		((AdminUserId = @AdminUserId) OR @AdminUserId IS NULL)
	AND (([Name] = @Name) OR @Name IS NULL)
	AND ((EmailAddress = @EmailAddress) OR @EmailAddress IS NULL)
	AND ((PhoneNumber = @PhoneNumber) OR @PhoneNumber IS NULL)
	AND IsDeleted = 0
END
