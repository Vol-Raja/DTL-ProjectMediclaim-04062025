USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[usp_LegalCasesDetails_Upsert]    Script Date: 27-08-2022 5.43.14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[usp_LegalCasesDetails_Upsert]  
(  
  @ID uniqueIdentifier = null   
 ,@CaseNo varchar(200)  
 ,@CourtType varchar(100)  
 ,@FileNumber varchar(100)
 ,@CaseInitialDate datetime = NULL  
 ,@NextHearingDate datetime = NULL  
 ,@PartiesInvolved varchar(500)  
 ,@NatureOfCase varchar(500)  
 ,@SummaryOfCase varchar(500)  
 ,@NameOfCouncil varchar(500)  
 ,@SubjectOfCase varchar(500)  
 ,@CaseEndDate datetime = NULL  
 ,@CaseResult varchar(500)  
 ,@CreatedBy nvarchar(max) = null     
 ,@ModifideBy nvarchar(max) = null 
 ,@PetitionerName nvarchar(255)
 ,@Email nvarchar(255)
 ,@Mobile nvarchar(15)
 ,@StatusType nvarchar(50)
 ,@Department nvarchar(255)
 ,@AttachmentFileInEnglish varbinary(max)
 ,@EnglishFileName nvarchar(250)
 ,@EnglishContentType nvarchar(250)
 ,@LegalAdvisorStatus nvarchar(255)
,@LegalAdvisorRemarks nvarchar(255)
 ,@ReturnMsg varchar(max) output  
)  
AS  
BEGIN  
  
BEGIN TRANSACTION  
  
BEGIN TRY  
IF(@Id IS NULL)  
BEGIN  
INSERT INTO dbo.LegalCases  
(  
  CaseNo  
 ,CourtType  
 ,FileNumber
 ,CaseInitialDate  
 ,NextHearingDate  
 ,PartiesInvolved  
 ,NatureOfCase  
 ,SummaryOfCase  
 ,NameOfCouncil  
 ,SubjectOfCase  
 ,CaseEndDate  
 ,CaseResult  
 ,CreatedBy  
 ,PetitionerName  
  ,Email  
  ,Mobile  
 , StatusType  
 , Department  
 , AttachmentFileInEnglish  
 , EnglishFileName  
 , EnglishContentType  
 ,LegalAdvisorStatus 
,LegalAdvisorRemarks 
)  
VALUES  
(  
     @CaseNo  
 ,@CourtType  
,@FileNumber
 ,@CaseInitialDate  
 ,@NextHearingDate  
 ,@PartiesInvolved  
 ,@NatureOfCase  
 ,@SummaryOfCase  
 ,@NameOfCouncil  
 ,@SubjectOfCase  
 ,@CaseEndDate  
 ,@CaseResult  
 ,@CreatedBy  
 ,@PetitionerName  
 ,@Email  
 ,@Mobile  
 ,@StatusType  
 ,@Department  
 ,@AttachmentFileInEnglish  
 ,@EnglishFileName  
 ,@EnglishContentType  
 ,@LegalAdvisorStatus 
,@LegalAdvisorRemarks  
)  
set @ReturnMsg ='add'  
  
END  
ELSE  
BEGIN  
UPDATE dbo.LegalCases  
SET   
  CaseNo = @CaseNo  
 ,CourtType = @CourtType  
 ,FileNumber = @FileNumber
 ,CaseInitialDate = @CaseInitialDate  
 ,NextHearingDate = @NextHearingDate  
 ,PartiesInvolved = @PartiesInvolved  
 ,NatureOfCase = @NatureOfCase  
 ,SummaryOfCase = @SummaryOfCase  
 ,NameOfCouncil = @NameOfCouncil  
 ,SubjectOfCase = @SubjectOfCase  
 ,CaseEndDate = @CaseEndDate  
 ,CaseResult = @CaseResult  
 ,ModifiedBy = @ModifideBy  
 ,ModifiedDate = GETDATE()  
 ,PetitionerName = @PetitionerName
 ,Email = @Email
 ,Mobile = @Mobile
 ,StatusType = @StatusType
 ,Department = @Department
 ,AttachmentFileInEnglish = @AttachmentFileInEnglish
 ,EnglishFileName = @EnglishFileName
 ,EnglishContentType = @EnglishContentType
 ,LegalAdvisorStatus = @LegalAdvisorStatus
,LegalAdvisorRemarks = @LegalAdvisorRemarks
WHERE ID = @Id  
set @ReturnMsg ='update'  
  
END  
  
COMMIT TRANSACTION  
  
END TRY  
BEGIN CATCH  
set @ReturnMsg = ERROR_MESSAGE()   
ROLLBACK TRANSACTION  
  
END CATCH  
  
END  
