USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[GetFooterLegalSectionByParam]    Script Date: 27-01-2023 11.32.55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetFooterLegalSectionByParam] (
	@Id UNIQUEIDENTIFIER = NULL
	,@IsDeleted BIT = NULL
	)
AS
BEGIN
	SELECT [ID]
		,CopyrightPolicy
		,CMAPPolicy
		,CAPPolicy
		,ContentReviewPolicy
		,Disclaimer
		,HyperlinkPolicy
		,TermsConditionPolicy
		,ContingencyManagementPlanPolicy
		,PrivacyPolicy
		,WebsiteMonitoringPlanPolicy
		,SecurityPolicy
		,[IsDeleted]
		,[CreatedDate]
		,[ModifiedDate]
		,[CreatedBy]
		,[ModifiedBy]
	FROM [DTLPension].[dbo].[FooterLegalSection]
	WHERE (
			(ID = @Id)
			OR @Id IS NULL
			)
		AND (
			(IsDeleted = @IsDeleted)
			OR @IsDeleted IS NULL
			)
END
