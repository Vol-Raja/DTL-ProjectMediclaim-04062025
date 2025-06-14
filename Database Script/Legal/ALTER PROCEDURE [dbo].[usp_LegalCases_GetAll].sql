USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[usp_LegalCases_GetAll]    Script Date: 27-08-2022 6.23.39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[usp_LegalCases_GetAll]  
  
AS  
  
BEGIN  
  
SELECT ID  
,CaseNo  
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
,CreatedDate  
,ModifiedDate  
,CreatedBy  
,ModifiedBy
,Approve
,IsDeleted
,PetitionerName 
 , Email  
 , Mobile 
 , StatusType 
 , Department  
  ,AttachmentFileInEnglish 
 , EnglishFileName 
 , EnglishContentType  
 ,LegalAdvisorStatus 
,LegalAdvisorRemarks 
FROM LegalCases  
  
END  
