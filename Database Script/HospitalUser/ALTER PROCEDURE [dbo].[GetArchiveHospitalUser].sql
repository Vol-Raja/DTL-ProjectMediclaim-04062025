USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetArchiveHospitalUser]    Script Date: 25-09-2022 9.36.36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetArchiveHospitalUser]

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
		IsDeleted = 1
END
