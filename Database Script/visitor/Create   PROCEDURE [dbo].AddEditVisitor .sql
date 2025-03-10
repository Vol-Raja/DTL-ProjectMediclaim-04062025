USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[AddEditEvent]    Script Date: 02-04-2023 6.11.50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




Create   PROCEDURE [dbo].AddEditVisitor      
    
  @Id uniqueIdentifier = null,
   
   @Note  [nvarchar](250),
	@VisitDate  [datetime],
	 

  @ReturnMsg nvarchar(max) output    
AS      
BEGIN     
    
begin try    
     
 INSERT INTO Visitor      
           (ID,
		   VisitDate,
		   Note)      
     VALUES      
           (@Id,
	@VisitDate,
	@Note)    
  set @ReturnMsg ='add'    
      
  
  end try    
 begin catch     
  set @ReturnMsg = ERROR_MESSAGE()     
    
  end catch    
END   
  
GO


