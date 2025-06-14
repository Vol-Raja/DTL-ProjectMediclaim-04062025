USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetFooterLegalSectionByParam]    Script Date: 29-12-2022 7.14.34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE  PROCEDURE [dbo].[GetFooterLegalSectionByParam]          
(          
 @Id  uniqueidentifier = NULL,             
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID],
     	CopyrightPolicy,
	CMAPPolicy,
	CAPPolicy,
	ContentReviewPolicy,
	Disclaimer,
	HyperlinkPolicy,
	TermsConditionPolicy ,
	ContingencyManagementPlanPolicy ,
	PrivacyPolicy,
	WebsiteMonitoringPlanPolicy  
      ,[IsDeleted]
    
	  
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[FooterLegalSection]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
    
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   