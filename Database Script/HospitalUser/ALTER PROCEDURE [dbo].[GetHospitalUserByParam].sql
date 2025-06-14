USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetHospitalUserByParam]    Script Date: 25-09-2022 9.33.04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetHospitalUserByParam]
(	
	@NameOfHospital NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@TypeOfHospital NVARCHAR(150),
	@PhoneNumber NVARCHAR(50),
	@HospitalUserId INT
)
As
BEGIN
	SELECT
		ID
		,HospitalUserId
		,NameOfHospital
		,EmailAddress
		,PhoneNumber
		,TypeOfHospital
		,AuthorizedPerson
		,Address
		,CreatedBy
		,CreatedDate
		,ModifiedBy
		,ModifiedDate
		,IsDeleted
		,Username
	FROM dbo.HospitalUser
	WHERE 
		((HospitalUserId = @HospitalUserId) OR @HospitalUserId IS NULL)
	AND ((NameOfHospital LIKE '%'+@NameOfHospital+'%') OR @NameOfHospital IS NULL)
	AND ((EmailAddress = @EmailAddress) OR @EmailAddress IS NULL)
	AND ((TypeOfHospital LIKE '%'+@TypeOfHospital+'%') OR @TypeOfHospital IS NULL)
	AND ((PhoneNumber =@PhoneNumber ) OR @PhoneNumber  IS NULL)
	AND IsDeleted = 0
END
