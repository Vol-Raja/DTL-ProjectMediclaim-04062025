USE [dtlpension1]
GO

/****** Object:  Table [dbo].[GPFGeneratePayment]    Script Date: 16-03-2022 20:13:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GPFGeneratePayment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationId] [nvarchar](12) NOT NULL,
	[NameOfEmployee] [nvarchar](60) NOT NULL,
	[ContactNo] [nvarchar](16) NULL,
	[LoanType] [nvarchar](60) NOT NULL,
	[NoOfEMI] [int] NOT NULL,
	[ApprovedAmount] [money] NOT NULL,
	[AccountNumber] [nvarchar](15) NULL,
	[IFSCCode] [nvarchar](10) NULL,
	[BICCode] [nvarchar](10) NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
	[IsDeleted] [bit] NULL
 CONSTRAINT [PK_PaymentId] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GPFGeneratePayment] ADD  CONSTRAINT [DF_GPFGeneratePayment_IsDelete]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[GPFGeneratePayment] ADD  CONSTRAINT [DF_GPFGeneratePayment_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO



CREATE PROCEDURE [dbo].[SaveGPFGeneratePayment]  
(  
 @ApplicationId nvarchar(12),  
 @NameOfEmployee nvarchar(60),  
 @ContactNo nvarchar(16),  
 @LoanType nvarchar(60),  
 @NoOfEMI int,  
 @ApprovedAmount money,  
 @AccountNumber nvarchar(15),  
 @IFSCCode nvarchar(10),  
 @BICCode nvarchar(10),  
 @CreatedBy nvarchar(30),  
 @PaymentId int output  
)  
AS  
BEGIN TRAN  
BEGIN TRY   
 INSERT INTO   
 [dbo].[GPFGeneratePayment]  
   (ApplicationId,  
    NameOfEmployee,  
    ContactNo,  
    LoanType,  
    NoOfEMI,  
    ApprovedAmount,  
    AccountNumber,  
    IFSCCode,  
    BICCode,  
    CreatedBy)  
     VALUES  
           (@ApplicationId,  
   @NameOfEmployee,  
   @ContactNo,  
   @LoanType,  
   @NoOfEMI,  
   @ApprovedAmount,  
   @AccountNumber,  
   @IFSCCode,  
   @BICCode,  
   @CreatedBy)  
 COMMIT TRAN    
  
 SET @PaymentId = SCOPE_IDENTITY();  
   
END TRY  
BEGIN CATCH  
ROLLBACK TRAN  
    -- Raise an error and return  
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
               @ErrorState -- State.    
               );    
END CATCH


CREATE PROCEDURE [dbo].[GetGPFGeneratePaymentByParam]
(
	@PaymentId int = NULL,
	@ApplicationId nvarchar(12) = NULL,
	@LoanType nvarchar(60) = NULL
)
AS
BEGIN 
	SELECT 
		PaymentId,
		ApplicationId,
		NameOfEmployee,
		ContactNo,
		LoanType,
		NoOfEMI,
		ApprovedAmount,
		AccountNumber,
		IFSCCode,
		BICCode,
		CreatedBy,
		CreatedDate,
		ModifiedBy,
		ModifiedDate
	FROM
		[dbo].[GPFGeneratePayment]
    WHERE
        ((ApplicationId = @ApplicationId) OR @ApplicationId IS NULL)
		AND ((PaymentId = @PaymentId) OR @PaymentId IS NULL)
		AND ((LoanType = @LoanType) OR @LoanType IS NULL)
		AND IsDeleted = 0
END


CREATE PROCEDURE [dbo].[DeleteGPFGeneratePayment]  
(   
 @PaymentId INT,
 @ModifiedBy NVARCHAR(60)
)  
AS  
BEGIN TRAN  
BEGIN TRY   
 UPDATE dbo.GPFGeneratePayment 
	SET IsDeleted = 1 
		,ModifiedBy = @ModifiedBy
		,ModifiedDate = GETDATE()
 WHERE PaymentId = @PaymentId;  
 COMMIT TRAN  
END TRY  
BEGIN CATCH  
   ROLLBACK TRAN  
    -- Raise an error and return  
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
               @ErrorState -- State.    
               );    
END CATCH  

CREATE PROCEDURE [dbo].[GetArchivedGPFGeneratePaymentByParam]
(
	@PaymentId int = NULL,
	@ApplicationId nvarchar(12) = NULL,
	@LoanType nvarchar(60) = NULL
)
AS
BEGIN 
	SELECT 
		PaymentId,
		ApplicationId,
		NameOfEmployee,
		ContactNo,
		LoanType,
		NoOfEMI,
		ApprovedAmount,
		AccountNumber,
		IFSCCode,
		BICCode,
		CreatedBy,
		CreatedDate,
		ModifiedBy,
		ModifiedDate
	FROM
		[dbo].[GPFGeneratePayment]
    WHERE
        ((ApplicationId = @ApplicationId) OR @ApplicationId IS NULL)
		AND ((PaymentId = @PaymentId) OR @PaymentId IS NULL)
		AND ((LoanType = @LoanType) OR @LoanType IS NULL)
		AND IsDeleted = 1
END