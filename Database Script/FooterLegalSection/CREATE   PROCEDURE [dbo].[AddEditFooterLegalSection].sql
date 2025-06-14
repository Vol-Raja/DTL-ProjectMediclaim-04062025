USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[AddEditFooterLegalSection]    Script Date: 29-12-2022 6.47.29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER   PROCEDURE [dbo].[AddEditFooterLegalSection]      
    
  @Id uniqueIdentifier = null,
  @CopyrightPolicy  [nvarchar](max),
   @CMAPPolicy  [nvarchar](max),
	
	@CAPPolicy  [nvarchar](max)  ,
	@ContentReviewPolicy  [nvarchar](max)  ,
	@Disclaimer [nvarchar](max)  ,
	@HyperlinkPolicy [nvarchar](max)  NULL,
	@TermsConditionPolicy  [nvarchar](max) NULL,
	@ContingencyManagementPlanPolicy [nvarchar](max)  ,
	@PrivacyPolicy [nvarchar](max)  NULL,
	@WebsiteMonitoringPlanPolicy [nvarchar](max) NULL,
	@IsDeleted  bit,
	 
	@CreatedBy [nvarchar] (50) = NULL  ,
	@ModifiedBy [nvarchar](50)  = NULL,
  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
if( @Id is null)    
begin    
   set @Id=newid()
 INSERT INTO [FooterLegalSection]      
           (ID,
	CopyrightPolicy,
	CMAPPolicy,
	CAPPolicy,
	ContentReviewPolicy,
	Disclaimer,
	HyperlinkPolicy,
	TermsConditionPolicy ,
	ContingencyManagementPlanPolicy ,
	PrivacyPolicy,
	WebsiteMonitoringPlanPolicy  ,
	
	IsDeleted,
	CreatedBy,
	ModifiedBy)      
     VALUES      
           (@Id,
	@CopyrightPolicy,
	@CMAPPolicy,
	 @CAPPolicy,
	@ContentReviewPolicy,
	@Disclaimer,
	@HyperlinkPolicy,
	@TermsConditionPolicy ,
	@ContingencyManagementPlanPolicy ,
	@PrivacyPolicy,
	@WebsiteMonitoringPlanPolicy  ,
	
	@IsDeleted,
	
	@CreatedBy,
	@ModifiedBy)    
  set @ReturnMsg ='add'    
  end    
  else    
  begin    
      
  Update  [FooterLegalSection]      
  set     
    CopyrightPolicy = @CopyrightPolicy,
	CMAPPolicy = @CMAPPolicy,
	CAPPolicy = @CAPPolicy,
 
	ContentReviewPolicy  = @ContentReviewPolicy,
	Disclaimer = @Disclaimer,
	HyperlinkPolicy = @HyperlinkPolicy,
	TermsConditionPolicy = @TermsConditionPolicy,
	ContingencyManagementPlanPolicy = @ContingencyManagementPlanPolicy,
	PrivacyPolicy = @PrivacyPolicy,
	WebsiteMonitoringPlanPolicy = @WebsiteMonitoringPlanPolicy  ,
	
	IsDeleted   = @IsDeleted,

	ModifiedBy = @ModifiedBy,
    ModifiedDate = GETDATE()    
     where Id = @Id   
        set @ReturnMsg ='update'    
  end    
  end try    
 begin catch     
  set @ReturnMsg = ERROR_MESSAGE()     
    
  end catch    
END   
  