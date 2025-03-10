USE [DTLPension]
GO

/****** Object:  Table [dbo].[GPFProcessing]    Script Date: 09-01-2022 20:52:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GPFProcessing](
	[GPFProcessingId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [nvarchar](15) NULL,
	[EmployeeName] [nvarchar](50) NULL,
	[Employer] [nvarchar](50) NULL,
	[Designation] [nvarchar](50) NULL,
	[MemberContribution] [money] NULL,
	[MemberInterest] [decimal](18, 0) NULL,
	[GPFAmount] [money] NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[EmployeeType] [nvarchar](9) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [date] NULL	
 CONSTRAINT [PK_GPFProcessingId] PRIMARY KEY CLUSTERED 
(
	[GPFProcessingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GPFProcessing] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO




/****** Object:  StoredProcedure [dbo].[SaveGPFProcessing]    Script Date: 09-01-2022 19:37:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SaveGPFProcessing]
(
	@xml XML,
	@CreatedBy NVARCHAR(50) 
)
AS
BEGIN TRANSACTION
BEGIN TRY
	 SET NOCOUNT ON;        
 
      INSERT INTO dbo.GPFProcessing
	   ([EmployeeNumber]
		,[EmployeeName]
		,[Employer]
		,[Designation]
		,[MemberContribution]
		,[MemberInterest]
		,[GPFAmount]
		,[Month]
		,[Year]
		,[EmployeeType]
		,[CreatedBy])
      SELECT
		GPF.value('(EmployeeNumber)[1]','VARCHAR(15)') AS EmployeeNumber, --TAG
		GPF.value('(EmployeeName)[1]','VARCHAR(50)') AS EmployeeName, --TAG
		GPF.value('(Employer)[1]','VARCHAR(50)') AS Employer, --TAG
		GPF.value('(Designation)[1]','VARCHAR(50)') AS Designation, --TAG
		GPF.value('(MemberContribution)[1]','MONEY') AS MemberContribution, --TAG
		GPF.value('(MemberInterest)[1]','DECIMAL') AS MemberInterest, --TAG
		GPF.value('(GPFAmount)[1]','MONEY') AS GPFAmount, --TAG
		GPF.value('(Month)[1]','INT') AS [Month], --TAG
		GPF.value('(Year)[1]','INT') AS [Year], --TAG
		GPF.value('(EmployeeType)[1]','VARCHAR(9)') AS [EmployeeType], --TAG
      @CreatedBy AS CreatedBy --TAG
	  FROM
      @xml.nodes('/ArrayOfGPFProcessing/GPFProcessing')AS TEMPTABLE(GPF)
	COMMIT TRANSACTION
END TRY
BEGIN CATCH 

ROLLBACK TRANSACTION
		DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;  
  
    SELECT   
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();  
  
    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState) -- State.
END CATCH


ALTER PROCEDURE [dbo].[GetGPFReportByParam]  
(  
 @Employer NVARCHAR(50) NULL,  
 @Month INT  = NULL,  
 @Year INT = NULL,  
 @EmployeeId NVARCHAR(15) = NULL,  
 @EmployeeName NVARCHAR(50) = NULL   
)  
AS  
BEGIN  
  SELECT [GPFProcessingId]  
    ,[EmployeeNumber]  
    ,[EmployeeName]  
    ,[Employer]  
    ,[Designation]  
    ,[MemberContribution]  
    ,[MemberInterest]  
    ,[GPFAmount]  
    ,[Month]  
    ,[Year]
	,[EmployeeType]
    ,[CreatedBy]  
    ,[CreatedDate]  
    ,[ModifiedBy]  
    ,[ModifiedDate]  
   FROM [dbo].[GPFProcessing]  
  
 WHERE   
 (([Month] = @Month) OR @Month IS NULL)  
 AND(([Year]=@Year) OR @Year IS NULL)  
 AND ((EmployeeNumber = @EmployeeId) OR @EmployeeId IS NULL)  
 AND ((Employer = @Employer) OR @Employer IS NULL)  
 AND ((EmployeeName = @EmployeeName) OR @EmployeeName IS NULL)  
END  
  