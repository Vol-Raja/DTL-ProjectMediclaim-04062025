USE [dtlpension1]
GO


ALTER TABLE dbo.GPFWithdraw ADD UniqueNumber NVARCHAR(12) NULL

/****** Object:  StoredProcedure [dbo].[SaveGPFWithdraw]    Script Date: 13-01-2022 12:44:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SaveGPFWithdraw]
(
	@WithdrawType NVARCHAR(13),
	@AccountHolderName NVARCHAR(50),
	@EmployeId NVARCHAR(15),
	@Designation NVARCHAR(20),
	@Employer NVARCHAR(100),
	@MonthlyGPFPay MONEY,
	@DateOfJoining DATE,
	@PurposeOfWithdrawal NVARCHAR(150),
	@MobileNumber NVARCHAR(10),
	@DependantsName NVARCHAR(50),
	@DependentsAge FLOAT,
	@DependentsAddress NVARCHAR(150),
	--Non Refundable--
	@TotalGPFContribution MONEY NULL,
	@TotalWithdrawalAmount MONEY NULL,
	@RemainingContribution MONEY NULL,
	--Refundable--
	@TotalAdvancedAmount MONEY NULL,
	@NoOfEMI INT NULL,
	@MonthlyDeduction MONEY NULL,
	@PurposeOfRefundable NVARCHAR(40),
	
	@PostingLastThreeYear NVARCHAR(50),
	@DateOfApplication DATE,
	@ReasonOfAdvance NVARCHAR(400) NULL,
	
	@BankAccountNo NVARCHAR(20),
	@IFSCNo NVARCHAR(30),
	@BranchName NVARCHAR(50),
	@BC NVARCHAR(20),
	@BankName NVARCHAR(50),
	
	@CreatedBy NVARCHAR(40),
	@ApplicationStatus INT,

	@WithdrawId  INT OUTPUT
)
AS
BEGIN
BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO dbo.GPFWithdraw
		(
			WithdrawType
			,AccountHolderName
			,EmployeId
			,Designation
			,Employer
			,MonthlyGPFPay
			,DateOfJoining
			,PurposeOfWithdrawal
			,MobileNumber
			,DependantsName
			,DependentsAge
			,DependentsAddress
			--Non Refundable--
			,TotalGPFContribution
			,TotalWithdrawalAmount
			,RemainingContribution
			--Refundable--
			,TotalAdvancedAmount
			,NoOfEMI
			,MonthlyDeduction
			,PurposeOfRefundable
			--
			,PostingLastThreeYear
			,DateOfApplication
			,ReasonOfAdvance
			--
			,BankAccountNo
			,IFSCNo
			,BranchName
			,BC
			,BankName
			--
			,CreatedBy
			,ApplicationStatus
		)
		VALUES
		(
			@WithdrawType
			,@AccountHolderName
			,@EmployeId
			,@Designation
			,@Employer
			,@MonthlyGPFPay
			,@DateOfJoining
			,@PurposeOfWithdrawal
			,@MobileNumber
			,@DependantsName
			,@DependentsAge
			,@DependentsAddress
			--Non Refundable--
			,@TotalGPFContribution
			,@TotalWithdrawalAmount
			,@RemainingContribution
			--Refundable--
			,@TotalAdvancedAmount
			,@NoOfEMI
			,@MonthlyDeduction
			,@PurposeOfRefundable
			--
			,@PostingLastThreeYear
			,@DateOfApplication
			,@ReasonOfAdvance
			--
			,@BankAccountNo
			,@IFSCNo
			,@BranchName
			,@BC
			,@BankName
			--
			,@CreatedBy
			,@ApplicationStatus
		)
		COMMIT TRANSACTION
		SET @WithdrawId = SCOPE_IDENTITY();

		UPDATE dbo.GPFWithdraw SET UniqueNumber =  
			CASE @WithdrawType
				WHEN  'Refundable' THEN CONCAT('R',@WithdrawId)
				WHEN  'NonRefundable' THEN CONCAT('NR',@WithdrawId)
				ELSE NULL
			END 
		WHERE WithdrawId = @WithdrawId

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
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
END
GO


USE [dtlpension1]
GO
/****** Object:  StoredProcedure [dbo].[GetGPFWithdrawalByParam]    Script Date: 14-01-2022 00:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetGPFWithdrawalByParam]  
(  
 @WithdrawId  INT = NULL,  
 @WithdrawType NVARCHAR(13) = NULL,  
 @AccountHolderName NVARCHAR(50) = NULL,  
 @EmployeId NVARCHAR(15) = NULL,  
 @Employer NVARCHAR(50) = NULL,  
 @DateOfJoining DATE = NULL,  
 @DateOfApplication DATE = NULL,  
 @Month INT = NULL,  
 @Year INT = NULL,  
 @ApplicationStatusId INT = NULL,  
 @CreatedBy NVARCHAR(40) = NULL,
 @Active BIT = 1
)  
AS  
BEGIN  
  
  SELECT     
   WithdrawId  
   ,WithdrawType  
   ,AccountHolderName  
   ,EmployeId  
   ,Designation  
   ,Employer  
   ,MonthlyGPFPay  
   ,DateOfJoining  
   ,PurposeOfWithdrawal  
   ,MobileNumber  
   ,DependantsName  
   ,DependentsAge  
   ,DependentsAddress  
   --Non Refundable--  
   ,TotalGPFContribution  
   ,TotalWithdrawalAmount  
   ,RemainingContribution  
   --Refundable--  
   ,TotalAdvancedAmount  
   ,NoOfEMI  
   ,MonthlyDeduction  
   ,PurposeOfRefundable  
   --  
   ,PostingLastThreeYear  
   ,DateOfApplication  
   ,ReasonOfAdvance  
   --  
   ,BankAccountNo  
   ,IFSCNo  
   ,BranchName  
   ,BC  
   ,BankName   
   ,ApplicationStatus
   ,PhysicalSubmit
   ,RejectReason
   ,CreatedBy  
   ,ModifiedBy  
   ,ModifiedDate
   ,UniqueNumber
  FROM dbo.GPFWithdraw  
  WHERE   
   ((WithdrawId = @WithdrawId) OR @WithdrawId IS NULL)  
   AND ((WithdrawType = @WithdrawType) OR @WithdrawType IS NULL)  
   AND ((AccountHolderName = @AccountHolderName) OR @AccountHolderName IS NULL)  
   AND ((EmployeId = @EmployeId) OR @EmployeId IS NULL)  
   AND ((Employer = @Employer) OR @Employer IS NULL)  
   AND (CONVERT(DATE,[DateOfJoining],102) = (CONVERT(DATE,@DateOfJoining,102)) OR @DateOfJoining IS NULL)  
   AND (CONVERT(DATE,DateOfApplication,102) = CONVERT(DATE,@DateOfApplication,102) OR @DateOfApplication IS NULL)  
   AND ((Month(DateOfApplication) = @Month) OR @Month IS NULL)  
   AND ((Year(DateOfApplication)=@Year) OR @Year IS NULL)  
   AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)  
   AND ((ApplicationStatus = @ApplicationStatusId) OR @ApplicationStatusId IS NULL)  
   AND ((Active = @Active) OR @Active IS NULL)
END  
  
