USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetDVBUserByParam]    Script Date: 25-09-2022 11.34.05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetDVBUserByParam]  
(   
 @Name NVARCHAR(60),  
 @EmailAddress NVARCHAR(120),  
 @Department NVARCHAR(100),  
 @Designation NVARCHAR(50),  
 @DVBUserId INT  
)  
As  
BEGIN  
 SELECT
 ID
  ,DVBUserId  
  ,[Name]  
  ,EmailAddress  
  ,PhoneNumber  
  ,Department  
  ,Designation  
  ,CreatedBy  
  ,CreatedDate  
  ,ModifiedBy  
  ,ModifiedDate
  ,[Address]  
,Username  
,Dashboard 
,DashboardUrl
,IsDeleted 
 FROM dbo.DVBUser  
 WHERE   
  ((DVBUserId = @DVBUserId) OR @DVBUserId IS NULL)  
 AND (([Name] = @Name) OR @Name IS NULL)  
 AND ((EmailAddress = @EmailAddress) OR @EmailAddress IS NULL)  
 AND ((Department = @Department) OR @Department IS NULL)  
 AND ((Designation =@Designation ) OR @Designation  IS NULL)  
END  
