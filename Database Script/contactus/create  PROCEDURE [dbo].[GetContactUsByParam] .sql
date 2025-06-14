USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetContactUsByParam]    Script Date: 17-10-2022 5.39.01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
create  PROCEDURE [dbo].[GetContactUsByParam]          
(          
 @Id  uniqueidentifier = NULL,             
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID],
     
	[Name],
	[NameHindi] ,
	[Designation] ,
	[DesignationHindi] ,
	[EmailAddress] ,
	[Telephone] ,
	[PhoneNumber]
	
      ,[IsDeleted]
    
	  
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[ContactUs]
       
  WHERE           
   ((ID = @Id) OR @Id IS NULL)          
    
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   