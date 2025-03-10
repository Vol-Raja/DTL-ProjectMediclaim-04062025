USE [master]
GO
/****** Object:  Database [DTLPension]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE DATABASE [DTLPension]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DTLPension', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DTLPension.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DTLPension_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DTLPension_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DTLPension] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DTLPension].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DTLPension] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DTLPension] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DTLPension] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DTLPension] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DTLPension] SET ARITHABORT OFF 
GO
ALTER DATABASE [DTLPension] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DTLPension] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DTLPension] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DTLPension] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DTLPension] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DTLPension] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DTLPension] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DTLPension] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DTLPension] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DTLPension] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DTLPension] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DTLPension] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DTLPension] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DTLPension] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DTLPension] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DTLPension] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DTLPension] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DTLPension] SET RECOVERY FULL 
GO
ALTER DATABASE [DTLPension] SET  MULTI_USER 
GO
ALTER DATABASE [DTLPension] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DTLPension] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DTLPension] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DTLPension] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DTLPension] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DTLPension] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DTLPension', N'ON'
GO
ALTER DATABASE [DTLPension] SET QUERY_STORE = OFF
GO
USE [DTLPension]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateAdmissiblePension]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalculateAdmissiblePension]
(
	@ChangeType varchar(50),
	@Pension decimal(18,6),
	@TDS decimal(18,6),
	@Recovery decimal(18,6),
	@Allowance decimal(18,6),
	@AQPAmount decimal(18,6)
)
RETURNS DECIMAL(18,6)
BEGIN 

DECLARE @RESULT DECIMAL(18,6)

SET @RESULT = (SELECT  @Pension - @TDS - @Recovery + @Allowance + @AQPAmount)

RETURN @RESULT

END
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdditionalQuantamPay]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalQuantamPay](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeRegistrationId] [uniqueidentifier] NOT NULL,
	[PensionerName] [varchar](50) NOT NULL,
	[DOB] [date] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[EmployerName] [varchar](500) NOT NULL,
	[CurrentAge] [int] NOT NULL,
	[AgeGroup] [varchar](50) NOT NULL,
	[MonthlyPension] [decimal](16, 8) NOT NULL,
	[AQPercentage] [varchar](50) NOT NULL,
	[IncrementedAmount] [decimal](16, 8) NOT NULL,
	[MonthlyAQPension] [decimal](16, 8) NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[fullName] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetDeclaration]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetDeclaration](
	[ID] [uniqueidentifier] NOT NULL,
	[FinancialYear] [varchar](50) NULL,
	[AllocationProgram] [varchar](500) NULL,
	[AllocatedFunds] [decimal](18, 2) NULL,
	[DisbursementPeriod] [varchar](100) NULL,
	[DisbursementAuthority] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[CreatedBy] [nchar](50) NULL,
	[ModifideBy] [nchar](50) NULL,
 CONSTRAINT [PK_BudgetDeclaration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClaimStatusMaster]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClaimStatusMaster](
	[Id] [int] NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClaimTypeMaster]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClaimTypeMaster](
	[Id] [int] NOT NULL,
	[Type] [nvarchar](11) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dispensary]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dispensary](
	[DispensaryId] [int] IDENTITY(1,1) NOT NULL,
	[DispensaryDate] [date] NOT NULL,
	[DispensaryLocation] [nvarchar](50) NOT NULL,
	[DoctorVisited] [nvarchar](30) NOT NULL,
	[TokenOPDNo] [nvarchar](30) NOT NULL,
	[MedicineAmount] [money] NOT NULL,
	[Investigation] [nvarchar](150) NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[NonCashlessClaimId] [int] NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[DispensaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeRegistration]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRegistration](
	[ID] [uniqueidentifier] NOT NULL,
	[PPOOrderId] [int] IDENTITY(1000,1) NOT NULL,
	[ProfileImage] [varbinary](max) NULL,
	[EmployeeId] [bigint] NOT NULL,
	[Prefix] [varchar](50) NULL,
	[EmployeeName] [varchar](50) NULL,
	[DOB] [date] NULL,
	[Gender] [varchar](50) NULL,
	[DTLOffice] [varchar](500) NULL,
	[Designation] [varchar](200) NULL,
	[ServiceStartDate] [date] NULL,
	[ServiceEndDate] [date] NULL,
	[ResidentialAddress] [varchar](max) NULL,
	[PermanentAddress] [varchar](max) NULL,
	[RState] [varchar](200) NULL,
	[RDistrict] [varchar](200) NULL,
	[RZipCode] [varchar](200) NULL,
	[PState] [varchar](200) NULL,
	[PDistrict] [varchar](200) NULL,
	[PZipCode] [varchar](200) NULL,
	[EmailAddress] [varchar](max) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Phone] [nvarchar](20) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifideBy] [varchar](50) NULL,
	[PPOStatusId] [int] NULL,
	[IsTemp] [bit] NULL,
	[ReasonOfRetirement] [varchar](500) NULL,
	[DeleteReason] [nvarchar](max) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_EmployeeRegistration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_EmployeeRegistration_UK] UNIQUE NONCLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Form5]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Form5](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeRegistrationId] [uniqueidentifier] NOT NULL,
	[Aadhar] [nvarchar](50) NULL,
	[PAN] [nvarchar](50) NULL,
	[Bank] [nvarchar](max) NULL,
	[BankAddress] [nvarchar](max) NULL,
	[IFSCCode] [nvarchar](50) NULL,
	[BICCode] [nvarchar](100) NULL,
	[IdentificationMark] [nvarchar](max) NULL,
	[SpecimenSignature] [varbinary](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifideBy] [nvarchar](50) NULL,
	[AadharCard] [varbinary](max) NULL,
	[PanCard] [varbinary](max) NULL,
	[BankAccountNumber] [varchar](max) NULL,
	[BankAccountName] [varchar](max) NULL,
	[SpecimenSignature2] [varbinary](max) NULL,
	[SpecimenSignature3] [varbinary](max) NULL,
	[ThumbImpression1] [varbinary](max) NULL,
	[ThumbImpression2] [varbinary](max) NULL,
 CONSTRAINT [PK_Form5] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GPFProcessData]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPFProcessData](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeName] [varchar](50) NOT NULL,
	[Employer] [varchar](500) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[Designation] [varchar](500) NOT NULL,
	[MemberContribution] [decimal](18, 2) NOT NULL,
	[MemberInterest] [decimal](18, 2) NOT NULL,
	[GPFAMount] [decimal](18, 2) NOT NULL,
	[Month] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GPFProcessing]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPFProcessing](
	[GPFProcessingId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [nvarchar](15) NULL,
	[EmployeeName] [nvarchar](30) NULL,
	[Employer] [nvarchar](30) NULL,
	[Designation] [nvarchar](30) NULL,
	[MemberContribution] [money] NULL,
	[MemberInterest] [decimal](18, 0) NULL,
	[GPFAmount] [money] NULL,
	[Month] [int] NULL,
	[CreatedBy] [nvarchar](30) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_GPFProcessingId] PRIMARY KEY CLUSTERED 
(
	[GPFProcessingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grievance]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grievance](
	[Id] [uniqueidentifier] NOT NULL,
	[Department] [varchar](100) NULL,
	[PPONo] [varchar](50) NULL,
	[EmployeeId] [bigint] NULL,
	[Subject] [varchar](500) NULL,
	[MobileNo] [varchar](100) NULL,
	[GrievanceDetails] [varchar](2000) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifideBy] [varchar](50) NULL,
 CONSTRAINT [PK_Grievance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvestmentDeclaration]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestmentDeclaration](
	[Id] [uniqueidentifier] NOT NULL,
	[FinancialYear] [varchar](50) NULL,
	[InvestmentType] [varchar](100) NULL,
	[InvestmentTitle] [varchar](500) NULL,
	[ReferenceNo] [varchar](100) NULL,
	[InvestedAmount] [decimal](18, 2) NULL,
	[ClosingPeriod] [varchar](50) NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[ExpectedProfitMargin] [decimal](18, 2) NULL,
	[ExpectedROI] [decimal](18, 2) NULL,
	[ActualReturnOnInvestment] [decimal](18, 2) NULL,
	[InvestmentClose] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifideBy] [varchar](50) NULL,
 CONSTRAINT [PK_InvestmentDeclaration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IPD]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IPD](
	[IPDId] [int] IDENTITY(1,1) NOT NULL,
	[DateOfAddmission] [date] NOT NULL,
	[HospitalName] [nvarchar](40) NOT NULL,
	[TreatmentIllsness] [nvarchar](100) NOT NULL,
	[DateOfDischarge] [date] NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[NonCashlessClaimId] [int] NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IPDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LegalCases]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LegalCases](
	[ID] [uniqueidentifier] NOT NULL,
	[CaseNo] [varchar](200) NOT NULL,
	[CourtType] [varchar](100) NOT NULL,
	[CaseInitialDate] [datetime] NOT NULL,
	[NextHearingDate] [datetime] NULL,
	[PartiesInvolved] [varchar](500) NOT NULL,
	[NatureOfCase] [varchar](500) NULL,
	[SummaryOfCase] [varchar](500) NULL,
	[NameOfCouncil] [varchar](500) NULL,
	[SubjectOfCase] [varchar](500) NULL,
	[CaseEndDate] [datetime] NULL,
	[CaseResult] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalCard]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalCard](
	[MedicalCardId] [int] IDENTITY(1,1) NOT NULL,
	[CardNo] [nvarchar](30) NOT NULL,
	[EmployeeNo] [nvarchar](30) NOT NULL,
	[PPONo] [nvarchar](30) NOT NULL,
	[NameOfCardHolder] [nvarchar](30) NOT NULL,
	[Employer] [nvarchar](30) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[MedicalSectionPageNo] [int] NOT NULL,
	[CardCategory] [nvarchar](13) NOT NULL,
	[MobileNumber] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
	[MedicalHistory] [nvarchar](150) NOT NULL,
	[BankName] [nvarchar](50) NOT NULL,
	[BICCode] [nvarchar](15) NOT NULL,
	[IFSCCode] [nvarchar](20) NOT NULL,
	[AccountNumber] [nvarchar](100) NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_MedicalCardId] PRIMARY KEY CLUSTERED 
(
	[MedicalCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicalPageDetail]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalPageDetail](
	[PageDetailId] [int] IDENTITY(1,1) NOT NULL,
	[PageNumber] [int] NOT NULL,
	[EmployeeNumber] [nvarchar](15) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[PPONumber] [nvarchar](30) NOT NULL,
	[Designation] [nvarchar](50) NOT NULL,
	[Department] [nvarchar](30) NOT NULL,
	[CardCategory] [nvarchar](13) NOT NULL,
	[MobileNumber] [nvarchar](10) NOT NULL,
	[DateOfRetirement] [date] NOT NULL,
	[SpouseName] [nvarchar](50) NULL,
	[LBP] [nvarchar](30) NULL,
	[Dispensary] [nvarchar](30) NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_PageDetailId] PRIMARY KEY CLUSTERED 
(
	[PageDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediclaimCashless]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediclaimCashless](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[NameOfHospital] [nvarchar](100) NOT NULL,
	[HospitalPhoneNumber] [nvarchar](16) NOT NULL,
	[HospitalAddress] [nvarchar](250) NOT NULL,
	[HospitalId] [nvarchar](15) NOT NULL,
	[EmailId] [nvarchar](50) NOT NULL,
	[NameOfPatient] [nvarchar](30) NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[PatientPhoneNumber] [nvarchar](16) NOT NULL,
	[PatientAddress] [nvarchar](250) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[MedicalSectionPageNumber] [int] NOT NULL,
	[NameOfCardHolder] [varchar](80) NOT NULL,
	[MedicalCardNumber] [nvarchar](16) NOT NULL,
	[AdmissionNumber] [int] NOT NULL,
	[CardCategory] [nvarchar](20) NOT NULL,
	[CaseType] [nvarchar](50) NOT NULL,
	[TypeOfTreatment] [nvarchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[DateOfAdmission] [date] NOT NULL,
	[DateOfDischargeOrDeath] [date] NOT NULL,
	[MedicalCard] [bit] NOT NULL,
	[DoctorPrescription] [bit] NOT NULL,
	[OriginalBill] [bit] NOT NULL,
	[BillCashMemo] [bit] NOT NULL,
	[ClaimStatusId] [int] NOT NULL,
	[RejectReason] [nvarchar](200) NULL,
	[PhysicalSubmit] [bit] NOT NULL,
	[ApprovedAmount] [money] NULL,
	[CreatedBy] [nvarchar](30) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MediclaimCashless] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediclaimNonCashless]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediclaimNonCashless](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [nvarchar](15) NOT NULL,
	[PPONumber] [nvarchar](10) NOT NULL,
	[MedicalSectionPageNumber] [int] NOT NULL,
	[MedicalCardHolderName] [nvarchar](30) NOT NULL,
	[Designation] [nvarchar](30) NOT NULL,
	[PatientName] [nvarchar](30) NOT NULL,
	[Gender] [nvarchar](6) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[ClaimFor] [nvarchar](9) NOT NULL,
	[RelationWithRetire] [nvarchar](8) NOT NULL,
	[MobileNumber] [nvarchar](10) NOT NULL,
	[CardCategory] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](60) NOT NULL,
	[ClaimType] [nvarchar](3) NOT NULL,
	[MedicalCardPhotoCopy] [bit] NOT NULL,
	[PrescriptionDetailPhotoCopy] [bit] NOT NULL,
	[OriginalBill] [bit] NOT NULL,
	[CashMemo] [bit] NOT NULL,
	[ClaimStatusId] [int] NOT NULL,
	[RejectReason] [nvarchar](200) NULL,
	[PhysicalSubmit] [bit] NOT NULL,
	[ApprovedAmount] [money] NULL,
	[CreatedBy] [nvarchar](30) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MediclaimVoucher]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MediclaimVoucher](
	[VoucherId] [int] IDENTITY(1,1) NOT NULL,
	[PayTo] [nvarchar](30) NOT NULL,
	[ApprovedAmount] [money] NOT NULL,
	[AmountInWords] [nvarchar](100) NULL,
	[EntryNo] [int] NOT NULL,
	[PageNo] [int] NOT NULL,
	[BankBranch] [nvarchar](100) NULL,
	[AccountNumber] [nvarchar](100) NULL,
	[BICCode] [nvarchar](15) NULL,
	[IFSCCode] [nvarchar](10) NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_VoucherId] PRIMARY KEY CLUSTERED 
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NomineeDetails]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NomineeDetails](
	[ID] [uniqueidentifier] NOT NULL,
	[MemberName] [nvarchar](200) NULL,
	[RelationShip] [nvarchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[ContigencyReason] [nvarchar](200) NULL,
	[GuardianName] [nvarchar](200) NULL,
	[Commutation] [int] NULL,
	[Arrear] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifideBy] [nvarchar](50) NULL,
	[MemberOf] [nvarchar](100) NULL,
	[EmployeeRegistrationId] [uniqueidentifier] NOT NULL,
	[GuardianRelation] [varchar](200) NULL,
	[GuardianAddress] [varchar](max) NULL,
 CONSTRAINT [PK_NomineeDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OPDCND]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OPDCND](
	[OPDCNDId] [int] IDENTITY(1,1) NOT NULL,
	[OPDCNDDate] [date] NOT NULL,
	[HospitalName] [nvarchar](40) NOT NULL,
	[MedicineAmount] [money] NOT NULL,
	[InvestigationAmount] [money] NOT NULL,
	[ConsultationAmount] [money] NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[HospitalizationClaimType] [nchar](3) NOT NULL,
	[NonCashlessClaimId] [int] NOT NULL,
	[CreatedBy] [nvarchar](30) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[OPDCNDId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PensionSlip]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PensionSlip](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeRegistrationId] [uniqueidentifier] NOT NULL,
	[ABPLast10Months] [decimal](18, 2) NULL,
	[EmolumentsForPension] [decimal](18, 2) NULL,
	[AdmissiableDate] [date] NULL,
	[AdmissiablePension] [decimal](18, 2) NULL,
	[PensionEnhancedRate] [decimal](18, 2) NULL,
	[AdmissibleForFromDate_Enhanced] [date] NULL,
	[AdmissibleForToDate_Enhanced] [date] NULL,
	[PensionAtNormalRate] [decimal](18, 2) NULL,
	[AdmissibleForFromDate_Normal] [date] NULL,
	[AdmissibleForToDate_Normal] [date] NULL,
	[Commutation] [decimal](18, 2) NULL,
	[CommutedPortion] [decimal](18, 2) NULL,
	[GratuityType] [nvarchar](200) NULL,
	[Gratuity] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifideBy] [nvarchar](50) NULL,
	[LeaveEncashment] [decimal](18, 2) NULL,
	[Taxable] [decimal](18, 2) NULL,
	[NonTaxable] [decimal](18, 2) NULL,
	[LeaveEncashmentDays] [int] NULL,
	[LumsumCommu] [decimal](18, 2) NULL,
	[TaxableCommu] [decimal](18, 2) NULL,
	[ServicePeriod] [varchar](500) NULL,
	[TaxableLeaveEncashment] [decimal](18, 2) NULL,
	[HRA] [decimal](18, 2) NULL,
	[Travel_Allowance] [decimal](18, 2) NULL,
	[Medical_Allowance] [decimal](18, 2) NULL,
	[Cess] [decimal](18, 2) NULL,
 CONSTRAINT [PK_PensionSlip] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PPOStatus]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PPOStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecoveryAllowance]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecoveryAllowance](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeRegistrationId] [uniqueidentifier] NOT NULL,
	[PensionerName] [varchar](50) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[EmployerName] [varchar](500) NOT NULL,
	[ChangeType] [varchar](500) NOT NULL,
	[Reason] [varchar](500) NOT NULL,
	[MonthlyPension] [decimal](16, 8) NOT NULL,
	[ApplicableAmount] [decimal](16, 8) NOT NULL,
	[MonthlyPensionAfter] [decimal](16, 8) NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[TypeOfRecovery] [varchar](500) NULL,
	[RecoveryAmount] [decimal](18, 2) NULL,
	[RecoveryOption] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePages]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](100) NOT NULL,
	[PageName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_RolePage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceHistory]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceHistory](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeRegistrationId] [uniqueidentifier] NOT NULL,
	[DTLDepartmentName] [nvarchar](300) NULL,
	[CategoryOfEmployeement] [nvarchar](300) NULL,
	[ReasonOfRetirement] [nvarchar](300) NULL,
	[IsMedicalCardRequired] [nvarchar](50) NULL,
	[TotalServiceYear] [int] NULL,
	[TotalServiceMonth] [int] NULL,
	[TotalServiceDays] [int] NULL,
	[AdditionalServiceYears] [int] NULL,
	[AdditionalServiceMonth] [int] NULL,
	[AdditionalServiceDays] [int] NULL,
	[ServiceNotCountedYear] [int] NULL,
	[ServiceNotCountedMonth] [int] NULL,
	[ServiceNotCountedDays] [int] NULL,
	[QualifyingServiceYear] [int] NULL,
	[QualifyingServiceMonth] [int] NULL,
	[QualifyingServiceDays] [int] NULL,
	[ServiceDetails] [nvarchar](max) NULL,
	[Perticulars] [nvarchar](max) NULL,
	[HalfYear] [nvarchar](max) NULL,
	[BasicPay] [numeric](18, 0) NULL,
	[DA] [numeric](18, 0) NULL,
	[NPA] [numeric](18, 0) NULL,
	[PaySlip1] [varbinary](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifideBy] [nvarchar](50) NULL,
	[PaySlip2] [varbinary](max) NULL,
	[PaySlip3] [varbinary](max) NULL,
	[Designation] [varchar](max) NULL,
	[LevelPayment] [int] NULL,
 CONSTRAINT [PK_ServiceHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TDSCalculation]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDSCalculation](
	[ID] [uniqueidentifier] NOT NULL,
	[EmployeeRegistrationId] [uniqueidentifier] NULL,
	[FinancialYear] [int] NULL,
	[Pensioner] [varchar](500) NULL,
	[Pension] [decimal](18, 2) NULL,
	[Gratuity] [decimal](18, 2) NULL,
	[Commutation] [decimal](18, 2) NULL,
	[AQP] [decimal](18, 2) NULL,
	[DA] [decimal](18, 2) NULL,
	[OtherIncome] [decimal](18, 2) NULL,
	[ExemptedAmount] [decimal](18, 2) NULL,
	[Ex80DD] [decimal](18, 2) NULL,
	[Ex80C] [decimal](18, 2) NULL,
	[Mediclaim] [decimal](18, 2) NULL,
	[HomeLoan] [decimal](18, 2) NULL,
	[IntOnEduLoan] [decimal](18, 2) NULL,
	[Donation] [decimal](18, 2) NULL,
	[TaxableAmount] [decimal](18, 2) NULL,
	[StandardDeduction] [decimal](18, 2) NULL,
	[NetTaxableAmount] [decimal](18, 2) NULL,
	[TaxPayable] [decimal](18, 2) NULL,
	[MonthlyTDSAmount] [decimal](18, 2) NULL,
	[CreatedDate] [date] NULL,
	[ModifyDate] [date] NULL,
	[CreatedBy] [nchar](50) NULL,
	[ModifideBy] [nchar](50) NULL,
	[Regime] [varchar](50) NULL,
	[Inv80GG] [decimal](18, 2) NULL,
	[InvFile80GG] [varbinary](max) NULL,
	[Inv80RRB] [decimal](18, 2) NULL,
	[InvFile80RRB] [varbinary](max) NULL,
	[Inv80TTA] [decimal](18, 2) NULL,
	[InvFile80TTA] [varbinary](max) NULL,
	[Inv80TTB] [decimal](18, 2) NULL,
	[InvFile80TTB] [varbinary](max) NULL,
	[Inv80U] [decimal](18, 2) NULL,
	[InvFile80U] [varbinary](max) NULL,
	[LeaveEncashment] [decimal](18, 2) NULL,
	[HRA] [decimal](18, 2) NULL,
	[Travel_Allowance] [decimal](18, 2) NULL,
	[Medical_Allowance] [decimal](18, 2) NULL,
	[Cess] [decimal](18, 2) NULL,
 CONSTRAINT [PK_TDSCalculation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TDSInvestment]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDSInvestment](
	[TDSInvestmentID] [uniqueidentifier] NOT NULL,
	[TDSCalculationId] [uniqueidentifier] NULL,
	[Inv80D] [decimal](18, 2) NULL,
	[InvFile80D] [varbinary](max) NULL,
	[Inv80DD] [decimal](18, 2) NULL,
	[InvFile80DD] [varbinary](max) NULL,
	[Inv80DDB] [decimal](18, 2) NULL,
	[InvFile80DDB] [varbinary](max) NULL,
	[Inv5YrsTermDepositPostoffice] [decimal](18, 2) NULL,
	[InvFile5YrsTermDepositPostoffice] [varbinary](max) NULL,
	[InvLIC_Pension_Plan] [decimal](18, 2) NULL,
	[InvFileLIC_Pension_Plan] [varbinary](max) NULL,
	[InvNSC] [decimal](18, 2) NULL,
	[InvFileNSC] [varbinary](max) NULL,
	[InvPPF] [decimal](18, 2) NULL,
	[InvFilePPF] [varbinary](max) NULL,
	[InvStampDuty] [decimal](18, 2) NULL,
	[InvFileStampDuty] [varbinary](max) NULL,
	[InvSukanyaSmriddhiYojana] [decimal](18, 2) NULL,
	[InvFileSukanyaSmriddhiYojana] [varbinary](max) NULL,
	[InvTermDepositBank] [decimal](18, 2) NULL,
	[InvFileTermDepositBank] [varbinary](max) NULL,
	[InvTF] [decimal](18, 2) NULL,
	[InvFileTF] [varbinary](max) NULL,
	[InvULIP] [decimal](18, 2) NULL,
	[InvFileULIP] [varbinary](max) NULL,
	[InvMF] [decimal](18, 2) NULL,
	[InvFileMF] [varbinary](max) NULL,
	[InvHousingLoanInt] [decimal](18, 2) NULL,
	[InvFileHousingLoanInt] [varbinary](max) NULL,
	[InvHousingLoanInt1617] [decimal](18, 2) NULL,
	[InvFileHousingLoanInt1617] [varbinary](max) NULL,
	[InvHousingLoanInt1920] [decimal](18, 2) NULL,
	[InvFileHousingLoanInt1920] [varbinary](max) NULL,
	[Inv80E] [decimal](18, 2) NULL,
	[InvFile80E] [varbinary](max) NULL,
	[Inv80G] [decimal](18, 2) NULL,
	[InvFile80G] [varbinary](max) NULL,
	[Inv80GGB] [decimal](18, 2) NULL,
	[InvFile80GGB] [varbinary](max) NULL,
	[Inv80GGC] [decimal](18, 2) NULL,
	[InvFile80GGC] [varbinary](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifideDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifideBy] [varchar](50) NULL,
	[Inv80GG] [decimal](18, 2) NULL,
	[InvFile80GG] [varbinary](max) NULL,
	[Inv80RRB] [decimal](18, 2) NULL,
	[InvFile80RRB] [varbinary](max) NULL,
	[Inv80TTA] [decimal](18, 2) NULL,
	[InvFile80TTA] [varbinary](max) NULL,
	[Inv80TTB] [decimal](18, 2) NULL,
	[InvFile80TTB] [varbinary](max) NULL,
	[Inv80U] [decimal](18, 2) NULL,
	[InvFile80U] [varbinary](max) NULL,
 CONSTRAINT [PK_TDSInvestment] PRIMARY KEY CLUSTERED 
(
	[TDSInvestmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_AdditionalQuantamPay]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdditionalQuantamPay] ON [dbo].[AdditionalQuantamPay]
(
	[EmployeeId] ASC
)
INCLUDE([PensionerName],[EmployerName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 12/15/2021 9:52:28 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdditionalQuantamPay] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[AdditionalQuantamPay] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [fullName]
GO
ALTER TABLE [dbo].[BudgetDeclaration] ADD  CONSTRAINT [DF_BudgetDeclaration_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Dispensary] ADD  CONSTRAINT [DF_Dispensary_CreatdDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmployeeRegistration] ADD  CONSTRAINT [DF_EmployeeRegistration_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[EmployeeRegistration] ADD  CONSTRAINT [DF_EmployeeRegistration_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmployeeRegistration] ADD  CONSTRAINT [DF__EmployeeR__PPOSt__68487DD7]  DEFAULT ((1)) FOR [PPOStatusId]
GO
ALTER TABLE [dbo].[EmployeeRegistration] ADD  CONSTRAINT [DF__EmployeeR__IsTem__1CBC4616]  DEFAULT ((0)) FOR [IsTemp]
GO
ALTER TABLE [dbo].[EmployeeRegistration] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Form5] ADD  CONSTRAINT [DF_Form5_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Form5] ADD  CONSTRAINT [DF_Form5_EmployeeRegistrationId]  DEFAULT (newid()) FOR [EmployeeRegistrationId]
GO
ALTER TABLE [dbo].[Form5] ADD  CONSTRAINT [DF_Form5_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[GPFProcessData] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[GPFProcessing] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Grievance] ADD  CONSTRAINT [DF_Grievance_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[InvestmentDeclaration] ADD  CONSTRAINT [DF_InvestmentDeclaration_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[IPD] ADD  CONSTRAINT [DF_IPD_CreatdDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[LegalCases] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[LegalCases] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MedicalCard] ADD  CONSTRAINT [DF__MedicalCa__Creat__6CA31EA0]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MedicalPageDetail] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_MedicalCard]  DEFAULT ((0)) FOR [MedicalCard]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_DoctorPrescription]  DEFAULT ((0)) FOR [DoctorPrescription]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_Bills]  DEFAULT ((0)) FOR [OriginalBill]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_PhotoId]  DEFAULT ((0)) FOR [BillCashMemo]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_ClaimStatusId]  DEFAULT ((1)) FOR [ClaimStatusId]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_PhysicalSubmit]  DEFAULT ((0)) FOR [PhysicalSubmit]
GO
ALTER TABLE [dbo].[MediclaimCashless] ADD  CONSTRAINT [DF_MediclaimCashless_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_MedicalCardPhotoCopy]  DEFAULT ((0)) FOR [MedicalCardPhotoCopy]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_PrescriptionDetailPhotoCopy]  DEFAULT ((0)) FOR [PrescriptionDetailPhotoCopy]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_OriginalBill]  DEFAULT ((0)) FOR [OriginalBill]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_CashMemo]  DEFAULT ((0)) FOR [CashMemo]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_ClaimStatusId]  DEFAULT ((1)) FOR [ClaimStatusId]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_PhysicalSubmit]  DEFAULT ((0)) FOR [PhysicalSubmit]
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MediclaimVoucher] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[NomineeDetails] ADD  CONSTRAINT [DF_NomineeDetails_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[OPDCND] ADD  CONSTRAINT [DF_OPDCND_CreatdDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PensionSlip] ADD  CONSTRAINT [DF_PensionSlip_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[PensionSlip] ADD  CONSTRAINT [DF_PensionSlip_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[RecoveryAllowance] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[RecoveryAllowance] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ServiceHistory] ADD  CONSTRAINT [DF_ServiceHistory_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[ServiceHistory] ADD  CONSTRAINT [DF_ServiceHistory_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TDSCalculation] ADD  CONSTRAINT [DF_TDSCalculation_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[TDSInvestment] ADD  CONSTRAINT [DF_TDSInvestment_ID]  DEFAULT (newid()) FOR [TDSInvestmentID]
GO
ALTER TABLE [dbo].[AdditionalQuantamPay]  WITH CHECK ADD  CONSTRAINT [FK__Additiona__Emplo__0C85DE4D] FOREIGN KEY([EmployeeRegistrationId])
REFERENCES [dbo].[EmployeeRegistration] ([ID])
GO
ALTER TABLE [dbo].[AdditionalQuantamPay] CHECK CONSTRAINT [FK__Additiona__Emplo__0C85DE4D]
GO
ALTER TABLE [dbo].[AdditionalQuantamPay]  WITH CHECK ADD  CONSTRAINT [FK__Additiona__Emplo__0D7A0286] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[EmployeeRegistration] ([EmployeeId])
GO
ALTER TABLE [dbo].[AdditionalQuantamPay] CHECK CONSTRAINT [FK__Additiona__Emplo__0D7A0286]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Form5]  WITH CHECK ADD  CONSTRAINT [FK_Form5_EmployeeRegistration] FOREIGN KEY([EmployeeRegistrationId])
REFERENCES [dbo].[EmployeeRegistration] ([ID])
GO
ALTER TABLE [dbo].[Form5] CHECK CONSTRAINT [FK_Form5_EmployeeRegistration]
GO
ALTER TABLE [dbo].[NomineeDetails]  WITH CHECK ADD  CONSTRAINT [FK_NomineeDetails_EmployeeRegistration] FOREIGN KEY([EmployeeRegistrationId])
REFERENCES [dbo].[EmployeeRegistration] ([ID])
GO
ALTER TABLE [dbo].[NomineeDetails] CHECK CONSTRAINT [FK_NomineeDetails_EmployeeRegistration]
GO
ALTER TABLE [dbo].[PensionSlip]  WITH CHECK ADD  CONSTRAINT [FK_PensionSlip_EmployeeRegistration] FOREIGN KEY([EmployeeRegistrationId])
REFERENCES [dbo].[EmployeeRegistration] ([ID])
GO
ALTER TABLE [dbo].[PensionSlip] CHECK CONSTRAINT [FK_PensionSlip_EmployeeRegistration]
GO
ALTER TABLE [dbo].[RecoveryAllowance]  WITH CHECK ADD  CONSTRAINT [FK__RecoveryA__Emplo__04E4BC85] FOREIGN KEY([EmployeeRegistrationId])
REFERENCES [dbo].[EmployeeRegistration] ([ID])
GO
ALTER TABLE [dbo].[RecoveryAllowance] CHECK CONSTRAINT [FK__RecoveryA__Emplo__04E4BC85]
GO
ALTER TABLE [dbo].[RecoveryAllowance]  WITH CHECK ADD  CONSTRAINT [FK__RecoveryA__Emplo__05D8E0BE] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[EmployeeRegistration] ([EmployeeId])
GO
ALTER TABLE [dbo].[RecoveryAllowance] CHECK CONSTRAINT [FK__RecoveryA__Emplo__05D8E0BE]
GO
ALTER TABLE [dbo].[ServiceHistory]  WITH CHECK ADD  CONSTRAINT [FK_ServiceHistory_EmployeeRegistration] FOREIGN KEY([EmployeeRegistrationId])
REFERENCES [dbo].[EmployeeRegistration] ([ID])
GO
ALTER TABLE [dbo].[ServiceHistory] CHECK CONSTRAINT [FK_ServiceHistory_EmployeeRegistration]
GO
ALTER TABLE [dbo].[TDSCalculation]  WITH CHECK ADD  CONSTRAINT [FK_TDSCalculation_EmployeeRegistration] FOREIGN KEY([EmployeeRegistrationId])
REFERENCES [dbo].[EmployeeRegistration] ([ID])
GO
ALTER TABLE [dbo].[TDSCalculation] CHECK CONSTRAINT [FK_TDSCalculation_EmployeeRegistration]
GO
ALTER TABLE [dbo].[TDSInvestment]  WITH CHECK ADD  CONSTRAINT [FK_TDSInvestment_TDSCalculation] FOREIGN KEY([TDSCalculationId])
REFERENCES [dbo].[TDSCalculation] ([ID])
GO
ALTER TABLE [dbo].[TDSInvestment] CHECK CONSTRAINT [FK_TDSInvestment_TDSCalculation]
GO
/****** Object:  StoredProcedure [dbo].[ApproveRejectCashlessClaim]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ApproveRejectCashlessClaim](
	@ClaimId INT,
	@ClaimStatusId INT,
	@RejectReason NVARCHAR(200) = NULL,
	@ApprovedAmount MONEY,
	@ModifiedBy  NVARCHAR(50)
)
AS
BEGIN TRAN
BEGIN TRY 

	UPDATE dbo.MediclaimCashless 
		SET ClaimStatusId = @ClaimStatusId,
		RejectReason = @RejectReason,
		ApprovedAmount =@ApprovedAmount,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETUTCDATE()
	WHERE
		ClaimId = @ClaimId
	
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
GO
/****** Object:  StoredProcedure [dbo].[ApproveRejectNonCashlessClaim]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ApproveRejectNonCashlessClaim](
	@ClaimId INT,
	@ClaimStatusId INT,
	@RejectReason NVARCHAR(200) = NULL,
	@ApprovedAmount MONEY,
	@ModifiedBy  NVARCHAR(50)
)
AS
BEGIN TRAN
BEGIN TRY 

	UPDATE dbo.MediclaimNonCashless 
		SET ClaimStatusId = @ClaimStatusId,
		RejectReason = @RejectReason,
		ApprovedAmount =@ApprovedAmount,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETUTCDATE()
	WHERE
		ClaimId = @ClaimId
	
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
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessClaimByParam]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCashlessClaimByParam](
	@Date Date = NULL,
	@ClaimId INT = NULL,
	@PageNumber INT = NULL 
)
AS
BEGIN
	SELECT [ClaimId],
	[NameOfHospital],
	[HospitalPhoneNumber],
	[HospitalAddress],
	[HospitalId],
	[EmailId],
	[NameOfPatient],
	[Gender],
	[PatientPhoneNumber],
	[PatientAddress],
	[DateOfBirth],
	[Age],
	[MedicalSectionPageNumber],
	[NameOfCardHolder],
	[MedicalCardNumber],
	[AdmissionNumber],
	[CardCategory],
	[CaseType],
	[TypeOfTreatment],
	[Amount],
	[DateOfAdmission],
	[DateOfDischargeOrDeath],
	[MedicalCard],
	[DoctorPrescription],
	[OriginalBill],
	[BillCashMemo],
	[ClaimStatusId],
	[RejectReason],
	[PhysicalSubmit],
	[ApprovedAmount],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate]
  FROM [dbo].[MediclaimCashless]
  WHERE 
	(CONVERT(DATE,[CreatedDate],102) = (CONVERT(DATE,@DATE,102)) OR @Date IS NULL)
	AND ((ClaimId = @ClaimId) OR @ClaimId IS NULL)
	AND ((MedicalSectionPageNumber = @PageNumber) OR @PageNumber IS NULL)
END
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByClaimId]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCashlessListByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT [ClaimId],
	[NameOfHospital],
	[HospitalPhoneNumber],
	[HospitalAddress],
	[HospitalId],
	[EmailId],
	[NameOfPatient],
	[Gender],
	[PatientPhoneNumber],
	[PatientAddress],
	[DateOfBirth],
	[Age],
	[MedicalSectionPageNumber],
	[NameOfCardHolder],
	[MedicalCardNumber],
	[AdmissionNumber],
	[CardCategory],
	[CaseType],
	[TypeOfTreatment],
	[Amount],
	[DateOfAdmission],
	[DateOfDischargeOrDeath],
	[MedicalCard],
	[DoctorPrescription],
	[OriginalBill],
	[BillCashMemo],
	[ClaimStatusId],
	[RejectReason],
	[PhysicalSubmit],
	[ApprovedAmount],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate]
  FROM [dbo].[MediclaimCashless]
  WHERE
	[CLaimId] = @ClaimId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCashlessListByCreatedBy]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCashlessListByCreatedBy]
(
	@CreatedBy nvarchar(30)
)
AS
BEGIN
SELECT [CLaimID]
      ,[NameOfHospital]
      ,[HospitalPhoneNumber]
      ,[HospitalAddress]
      ,[HospitalId]
      ,[EmailId]
      ,[NameOfPatient]
      ,[Gender]
      ,[PatientPhoneNumber]
      ,[PatientAddress]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardNumber]
      ,[AdmissionNumber]
      ,[CardCategory]
      ,[CaseType]
      ,[TypeOfTreatment]
      ,[DateOfAdmission]
      ,[DateOfDischargeOrDeath]
      ,[MedicalCard]
      ,[DoctorPrescription]
      ,[OriginalBill]
      ,[BillCashMemo]
	  ,[ClaimStatusId]
	  ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[MediclaimCashless]
  WHERE
	[CreatedBy] = @CreatedBy
END
GO
/****** Object:  StoredProcedure [dbo].[GetDispensaryByClaimId]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDispensaryByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT
	   [DispensaryId]
      ,[DispensaryDate]
      ,[DispensaryLocation]
      ,[DoctorVisited]
      ,[TokenOPDNo]
      ,[MedicineAmount]
      ,[Investigation]
      ,[TotalAmount]
      ,[NonCashlessClaimId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM 
	[dbo].[Dispensary]
  WHERE 
	[NonCashlessClaimId] = @ClaimId
END
GO
/****** Object:  StoredProcedure [dbo].[GetGPFReportByParam]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [dbo].[GetGPFReportByParam]
(
	@Employer NVARCHAR(30) NULL,
	@Month INT  = NULL,
	@Year INT = NULL,
	@EmployeeId NVARCHAR(15) = NULL 
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
		  ,[CreatedBy]
		  ,[CreatedDate]
		  ,[ModifiedBy]
		  ,[ModifiedDate]
	  FROM [dbo].[GPFProcessing]

	WHERE 
	(([Month] = @Month) OR @Month IS NULL)
	AND((YEAR(CreatedDate)=@Year) OR @Year IS NULL)
	AND ((EmployeeNumber = @EmployeeId) OR @EmployeeId IS NULL)
	AND ((Employer = @Employer) OR @Employer IS NULL)
END
GO
/****** Object:  StoredProcedure [dbo].[GetIPDByClaimId]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetIPDByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT
	   [IPDId]
      ,[DateOfAddmission]
      ,[HospitalName]
      ,[TreatmentIllsness]
      ,[DateOfDischarge]
      ,[TotalAmount]
      ,[NonCashlessClaimId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[IPD]
  WHERE 
	[NonCashlessClaimId] = @ClaimId
END
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalCardByParam]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMedicalCardByParam]
(
	@MedicalCardId [INT] = NULL,
	@CardNo [NVARCHAR](30) = NULL,
	@EmployeeNo [NVARCHAR](30) = NULL,
	@PPONo [NVARCHAR](30) = NULL,
	@CreatedBy [NVARCHAR](30) = NULL
)
AS
BEGIN
	SELECT
	
		MedicalCardId,
		CardNo,
		EmployeeNo,
		PPONo,
		NameOfCardHolder,
		Employer,
		DateOfBirth,
		Age,
		Gender,
		MedicalSectionPageNo,
		CardCategory,
		MobileNumber,
		[Address],
		MedicalHistory,
		BankName,
		BICCode,
		IFSCCode,
		AccountNumber,
		CreatedBy,
		ModifiedBy
	
	FROM dbo.MedicalCard
	WHERE
	
		((MedicalCardId = @MedicalCardId) OR @MedicalCardId IS NULL)
		AND ((CardNo = @CardNo) OR @CardNo IS NULL)
		AND ((EmployeeNo = @EmployeeNo) OR @EmployeeNo IS NULL)
		AND ((PPONo = @PPONo) OR @PPONo IS NULL)
		AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetMedicalPageDetailByParam]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMedicalPageDetailByParam]
	(
		@PageDetailId INT = NULL,
		@EmployeeNumber NVARCHAR(15) = NULL,
		@PPONumber NVARCHAR(30) = NULL,
		@Department NVARCHAR(30) = NULL,
		@PageNumber INT = NULL,
		@CreatedBy NVARCHAR(40) = NULL
	)
	AS
	BEGIN
		SELECT
			 PageDetailId,
			 PageNumber,
			 EmployeeNumber,
			 Name,
			 PPONumber,
			 Designation,
			 Department,
			 CardCategory,
			 MobileNumber,
			 DateOfRetirement,
			 SpouseName,
			 LBP,
			 Dispensary,
			 CreatedBy,
			 CreatedDate,
			 ModifiedBy,
			 ModifiedDate
	
		FROM dbo.MedicalPageDetail
		WHERE
			((PageNumber = @PageNumber) OR @PageNumber IS NULL)
			AND ((EmployeeNumber LIKE '%'+@EmployeeNumber+'%') OR @EmployeeNumber IS NULL)
			AND ((PPONumber LIKE '%'+@PPONumber+'%') OR @PPONumber IS NULL)
			AND ((Department LIKE '%'+@Department+'%') OR @Department IS NULL)
			AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
			AND ((PageDetailId = @PageDetailId) OR @PageDetailId IS NULL)
		
	
	END
GO
/****** Object:  StoredProcedure [dbo].[GetMediclaimVoucherByParam]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetMediclaimVoucherByParam]
(
	@VoucherId [INT] = NULL,
	@PayTo [NVARCHAR](30) = NULL,
	@EntryNo [INT] =NULL,
	@PageNo [INT] = NULL,
	@CreatedBy [NVARCHAR](30) = NULL
)
AS
BEGIN
	SELECT
	
		VoucherId,
		PayTo,
		ApprovedAmount,
		AmountInWords,
		EntryNo,
		PageNo,
		BankBranch,
		AccountNumber,
		BICCode,
		IFSCCode	
	
	FROM dbo.MediclaimVoucher
	WHERE
		((VoucherId = @VoucherId) OR @VoucherId IS NULL)
		AND ((PayTo LIKE '%'+@PayTo+'%') OR @PayTo IS NULL)
		AND ((EntryNo = @EntryNo) OR @EntryNo IS NULL)
		AND ((PageNo = @PageNo) OR @PageNo IS NULL)
		AND ((CreatedBy = @CreatedBy) OR @CreatedBy IS NULL)
	
END

GO
/****** Object:  StoredProcedure [dbo].[GetNonCashlessClaimByParam]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNonCashlessClaimByParam](
	@Date Date = NULL,
	@ClaimId INT = NULL,
	@PageNumber INT = NULL 
)
AS
BEGIN
	SELECT [ClaimId]
      ,[EmployeeNumber]
      ,[PPONumber]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardHolderName]
      ,[Designation]
      ,[PatientName]
      ,[Gender]
      ,[DateOfBirth]
      ,[Age]
      ,[ClaimFor]
      ,[RelationWithRetire]
      ,[MobileNumber]
      ,[CardCategory]
      ,[Address]
      ,[ClaimType]
      ,[MedicalCardPhotoCopy]
      ,[PrescriptionDetailPhotoCopy]
      ,[OriginalBill]
      ,[CashMemo]
      ,[ClaimStatusId]
      ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[MediclaimNonCashless]
  WHERE 
	(CONVERT(DATE,[CreatedDate],102) = (CONVERT(DATE,@DATE,102)) OR @Date IS NULL)
	AND ((ClaimId = @ClaimId) OR @ClaimId IS NULL)
	AND ((MedicalSectionPageNumber = @PageNumber) OR @PageNumber IS NULL)
END
GO
/****** Object:  StoredProcedure [dbo].[GetNonCashlessDetailByClaimId]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNonCashlessDetailByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT [ClaimId]
      ,[EmployeeNumber]
      ,[PPONumber]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardHolderName]
      ,[Designation]
      ,[PatientName]
      ,[Gender]
      ,[DateOfBirth]
      ,[Age]
      ,[ClaimFor]
	  ,[RelationWithRetire]
      ,[MobileNumber]
      ,[CardCategory]
      ,[Address]
      ,[ClaimType]
      ,[MedicalCardPhotoCopy]
      ,[PrescriptionDetailPhotoCopy]
      ,[OriginalBill]
      ,[CashMemo]
      ,[ClaimStatusId]
      ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[MediclaimNonCashless]
  WHERE
	[ClaimId] = @ClaimId
END
GO
/****** Object:  StoredProcedure [dbo].[GetNonCashlessListByCreatedBy]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNonCashlessListByCreatedBy]
(
	@CreatedBy nvarchar(30)
)
AS
BEGIN
SELECT [ClaimId]
      ,[EmployeeNumber]
      ,[PPONumber]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardHolderName]
      ,[Designation]
      ,[PatientName]
      ,[Gender]
      ,[DateOfBirth]
      ,[Age]
      ,[ClaimFor]
	  ,[RelationWithRetire]
      ,[MobileNumber]
      ,[CardCategory]
      ,[Address]
      ,[ClaimType]
      ,[MedicalCardPhotoCopy]
      ,[PrescriptionDetailPhotoCopy]
      ,[OriginalBill]
      ,[CashMemo]
      ,[ClaimStatusId]
      ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[MediclaimNonCashless]
  WHERE
	[CreatedBy] = @CreatedBy
END
GO
/****** Object:  StoredProcedure [dbo].[GetOPDCNDByClaimId]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOPDCNDByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT [OPDCNDId]
      ,[OPDCNDDate]
      ,[HospitalName]
      ,[MedicineAmount]
      ,[InvestigationAmount]
      ,[ConsultationAmount]
      ,[TotalAmount]
      ,[HospitalizationClaimType]
      ,[NonCashlessClaimId]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[OPDCND]
  WHERE 
	[NonCashlessClaimId] = @ClaimId
END
GO
/****** Object:  StoredProcedure [dbo].[GetPendingCashlessClaims]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPendingCashlessClaims]
AS
BEGIN
	SELECT [ClaimId],
	[NameOfHospital],
	[HospitalPhoneNumber],
	[HospitalAddress],
	[HospitalId],
	[EmailId],
	[NameOfPatient],
	[Gender],
	[PatientPhoneNumber],
	[PatientAddress],
	[DateOfBirth],
	[Age],
	[MedicalSectionPageNumber],
	[NameOfCardHolder],
	[MedicalCardNumber],
	[AdmissionNumber],
	[CardCategory],
	[CaseType],
	[TypeOfTreatment],
	[Amount],
	[DateOfAdmission],
	[DateOfDischargeOrDeath],
	[MedicalCard],
	[DoctorPrescription],
	[OriginalBill],
	[BillCashMemo],
	[ClaimStatusId],
	[RejectReason],
	[PhysicalSubmit],
	[ApprovedAmount],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate]
  FROM [DTLPension].[dbo].[MediclaimCashless]
  WHERE ClaimStatusId = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetPendingNonCashlessClaims]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPendingNonCashlessClaims]
AS
BEGIN
	SELECT [ClaimId]
      ,[EmployeeNumber]
      ,[PPONumber]
      ,[MedicalSectionPageNumber]
      ,[MedicalCardHolderName]
      ,[Designation]
      ,[PatientName]
      ,[Gender]
      ,[DateOfBirth]
      ,[Age]
      ,[ClaimFor]
      ,[RelationWithRetire]
      ,[MobileNumber]
      ,[CardCategory]
      ,[Address]
      ,[ClaimType]
      ,[MedicalCardPhotoCopy]
      ,[PrescriptionDetailPhotoCopy]
      ,[OriginalBill]
      ,[CashMemo]
      ,[ClaimStatusId]
      ,[RejectReason]
	  ,[PhysicalSubmit]
	  ,[ApprovedAmount]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [DTLPension].[dbo].[MediclaimNonCashless]
  WHERE ClaimStatusId = 1
END
GO
/****** Object:  StoredProcedure [dbo].[SaveDispensary]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveDispensary]
(	
	@DispensaryDate date,
	@DispensaryLocation nvarchar(50),
	@DoctorVisited nvarchar(30),
	@TokenOPDNo nvarchar(30),
	@MedicineAmount money,
	@Investigation nvarchar(150),
	@TotalAmount money,
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30)
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[Dispensary]
	           ([DispensaryDate]
			   ,[DispensaryLocation]
	           ,[DoctorVisited]
	           ,[TokenOPDNo]
	           ,[MedicineAmount]
	           ,[Investigation]
	           ,[TotalAmount]
	           ,[NonCashlessClaimId]
	           ,[CreatedBy])
	     VALUES
	           (@DispensaryDate
			   ,@DispensaryLocation
	           ,@DoctorVisited
	           ,@TokenOPDNo
	           ,@MedicineAmount
	           ,@Investigation
	           ,@TotalAmount
	           ,@NonCashlessClaimId
	           ,@CreatedBy)
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
GO
/****** Object:  StoredProcedure [dbo].[SaveGPFProcessing]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveGPFProcessing]
(
	@xml XML,
	@CreatedBy NVARCHAR(30) 
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
		,[CreatedBy])
      SELECT
		GPF.value('(EmployeeNumber)[1]','VARCHAR(30)') AS EmployeeNumber, --TAG
		GPF.value('(EmployeeName)[1]','VARCHAR(30)') AS EmployeeName, --TAG
		GPF.value('(Employer)[1]','VARCHAR(30)') AS Employer, --TAG
		GPF.value('(Designation)[1]','VARCHAR(30)') AS Designation, --TAG
		GPF.value('(Contribution)[1]','MONEY') AS MemberContribution, --TAG
		GPF.value('(Interest)[1]','DECIMAL') AS MemberInterest, --TAG
		GPF.value('(GPFAmount)[1]','MONEY') AS GPFAmount, --TAG
		GPF.value('(Month)[1]','INT') AS [Month], --TAG
      @CreatedBy AS CreatedBy --TAG
	  FROM
      @xml.nodes('/ArrayOfGPFProcessingModel/GPFProcessingModel')AS TEMPTABLE(GPF)
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
GO
/****** Object:  StoredProcedure [dbo].[SaveIPD]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveIPD]
(	
	@DateOfAddmission date,
	@HospitalName nvarchar(40),
	@TreatmentIllsness nvarchar(150),
	@DateOfDischarge date,
	@TotalAmount money,
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30)
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[IPD]
	           ([DateOfAddmission]
	           ,[HospitalName]
	           ,[TreatmentIllsness]
	           ,[DateOfDischarge]
	           ,[TotalAmount]
	           ,[NonCashlessClaimId]
	           ,[CreatedBy])
	     VALUES
	           (@DateOfAddmission
	           ,@HospitalName
	           ,@TreatmentIllsness
	           ,@DateOfDischarge
	           ,@TotalAmount
	           ,@NonCashlessClaimId
	           ,@CreatedBy)
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
GO
/****** Object:  StoredProcedure [dbo].[SaveMedicalCard]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveMedicalCard]
(
	@CardNo [NVARCHAR](30),
	@EmployeeNo [NVARCHAR](30),
	@PPONo [NVARCHAR](30),
	@NameOfCardHolder [NVARCHAR](30),
	@Employer [NVARCHAR](30),
	@DateOfBirth [DATE],
	@Age [INT],
	@Gender [NVARCHAR](6),
	@MedicalSectionPageNo [INT],
	@CardCategory [NVARCHAR](13),
	@MobileNumber [NVARCHAR](10),
	@Address [NVARCHAR](150),
	@MedicalHistory [NVARCHAR](150),
	@BankName [NVARCHAR](50),
	@BICCode [NVARCHAR](15),
	@IFSCCode [NVARCHAR](20),
	@AccountNumber[NVARCHAR](100),
	@CreatedBy [NVARCHAR](30),
	@MedicalCardId [INT] OUTPUT
)
AS
BEGIN TRAN
BEGIN TRY
	INSERT INTO dbo.MedicalCard
	(
		CardNo,
		EmployeeNo,
		PPONo,
		NameOfCardHolder,
		Employer,
		DateOfBirth,
		Age,
		Gender,
		MedicalSectionPageNo,
		CardCategory,
		MobileNumber,
		[Address],
		MedicalHistory,
		BankName,
		BICCode,
		IFSCCode,
		AccountNumber,
		CreatedBy)
	 VALUES(
		@CardNo,
		@EmployeeNo,
		@PPONo,
		@NameOfCardHolder,
		@Employer,
		@DateOfBirth,
		@Age,
		@Gender,
		@MedicalSectionPageNo,
		@CardCategory,
		@MobileNumber,
		@Address,
		@MedicalHistory,
		@BankName,
		@BICCode,
		@IFSCCode,
		@AccountNumber,
		@CreatedBy)
	 COMMIT TRAN
	 SET @MedicalCardId = SCOPE_IDENTITY();
END TRY
BEGIN CATCH
	ROLLBACK TRAN
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
GO
/****** Object:  StoredProcedure [dbo].[SaveMedicalPageDetail]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveMedicalPageDetail]
(
	@PageNumber INT,
	@EmployeeNumber NVARCHAR(15),
	@Name NVARCHAR(30),
	@PPONumber NVARCHAR(30),
	@Designation NVARCHAR(50),
	@Department NVARCHAR(30),
	@CardCategory NVARCHAR(13),
	@MobileNumber NVARCHAR(10),
	@DateOfRetirement DATE,
	@SpouseName NVARCHAR(50),
	@LBP NVARCHAR(30),
	@Dispensary NVARCHAR(30),
	@CreatedBy NVARCHAR(50),
	@PageDetailId INT OUTPUT
)
AS
BEGIN TRANSACTION
BEGIN TRY
	INSERT INTO dbo.MedicalPageDetail
		(
		 PageNumber,
		 EmployeeNumber,
		 Name,
		 PPONumber,
		 Designation,
		 Department,
		 CardCategory,
		 MobileNumber,
		 DateOfRetirement,
		 SpouseName,
		 LBP,
		 Dispensary,
		 CreatedBy)
	VALUES
		 (
			@PageNumber,
			@EmployeeNumber,
			@Name,
			@PPONumber,
			@Designation,
			@Department,
			@CardCategory,
			@MobileNumber,
			@DateOfRetirement,
			@SpouseName,
			@LBP,
			@Dispensary,
			@CreatedBy)
	COMMIT TRANSACTION
	
	 SET @PageDetailId = SCOPE_IDENTITY();
	
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
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
GO
/****** Object:  StoredProcedure [dbo].[SaveMediclaimCashless]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveMediclaimCashless]
(
	 @NameOfHospital nvarchar(100)
	,@HospitalPhoneNumber nvarchar(16) 
	,@HospitalAddress nvarchar(250)
	,@HospitalId nvarchar(15)
	,@EmailId nvarchar(50)
	,@NameOfPatient nvarchar(30)
	,@Gender nvarchar(6)
	,@PatientPhoneNumber nvarchar(16)
	,@PatientAddress nvarchar(250)
	,@DateOfBirth date
	,@Age int
	,@MedicalSectionPageNumber int
	,@NameOfCardHolder nvarchar(80)
	,@MedicalCardNumber nvarchar(16)
	,@AdmissionNumber int
	,@CardCategory nvarchar(20)
	,@CaseType nvarchar(50)
	,@TypeOfTreatment nvarchar(50)
	,@Amount money
	,@DateOfAdmission date
	,@DateOfDischargeOrDeath date
	,@MedicalCard bit
	,@DoctorPrescription bit
	,@OriginalBill bit
	,@BillCashMemo bit
	,@CreatedBy nvarchar(30) NULL
	,@ClaimId int output
)
AS
BEGIN TRAN
BEGIN TRY 
	INSERT INTO [dbo].[MediclaimCashless]
			   ([NameOfHospital]
			   ,[HospitalPhoneNumber]
			   ,[HospitalAddress]
			   ,[HospitalId]
			   ,[EmailId]
			   ,[NameOfPatient]
			   ,[Gender]
			   ,[PatientPhoneNumber]
			   ,[PatientAddress]
			   ,[DateOfBirth]
			   ,[Age]
			   ,[MedicalSectionPageNumber]
			   ,[NameOfCardHolder]
			   ,[MedicalCardNumber]
			   ,[AdmissionNumber]
			   ,[CardCategory]
			   ,[CaseType]
			   ,[TypeOfTreatment]
			   ,[Amount]
			   ,[DateOfAdmission]
			   ,[DateOfDischargeOrDeath]
			   ,[MedicalCard]
			   ,[DoctorPrescription]
			   ,[OriginalBill]
			   ,[BillCashMemo]
			   ,[CreatedBy])
		 VALUES
			   (@NameOfHospital
				,@HospitalPhoneNumber 
				,@HospitalAddress
				,@HospitalId
				,@EmailId
				,@NameOfPatient
				,@Gender
				,@PatientPhoneNumber
				,@PatientAddress
				,@DateOfBirth
				,@Age
				,@MedicalSectionPageNumber
				,@NameOfCardHolder
				,@MedicalCardNumber
				,@AdmissionNumber
				,@CardCategory
				,@CaseType
				,@TypeOfTreatment
				,@Amount
				,@DateOfAdmission
				,@DateOfDischargeOrDeath
				,@MedicalCard
				,@DoctorPrescription
				,@OriginalBill
				,@BillCashMemo
				,@CreatedBy)
			COMMIT TRAN
			
			SET @ClaimId = SCOPE_IDENTITY();
	
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
GO
/****** Object:  StoredProcedure [dbo].[SaveMediclaimNonCashless]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveMediclaimNonCashless]
(
	@EmployeeNumber nvarchar(15),
	@PPONumber nvarchar(10),
	@MedicalSectionPageNumber int,	
	@MedicalCardHolderName nvarchar(30),
	@Designation nvarchar(30),
	@PatientName nvarchar(30),
	@Gender nvarchar(6),
	@DateOfBirth date,
	@Age int,
	@ClaimFor nvarchar(9),
	@RelationWithRetire nvarchar(8) NULL,
	@MobileNumber nvarchar(10),
	@CardCategory nvarchar(20),
	@Address nvarchar(60),
	@ClaimType nvarchar (3),
	@MedicalCardPhotoCopy bit,
	@PrescriptionDetailPhotoCopy bit ,
	@OriginalBill bit,
	@CashMemo bit,
	@ClaimStatusId int,
	@CreatedBy nvarchar(30) NULL,
	@ClaimId int output
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[MediclaimNonCashless]
			([EmployeeNumber]
			,[PPONumber]
			,[MedicalSectionPageNumber]	
			,[MedicalCardHolderName]
			,[Designation]
			,[PatientName]
			,[Gender]
			,[DateOfBirth]
			,[Age]
			,[ClaimFor]
			,[RelationWithRetire]
			,[MobileNumber]
			,[CardCategory]
			,[Address]
			,[ClaimType]
			,[MedicalCardPhotoCopy]
			,[PrescriptionDetailPhotoCopy]
			,[OriginalBill]
			,[CashMemo]
			,[ClaimStatusId]
			,[CreatedBy])
     VALUES
           (@EmployeeNumber
			,@PPONumber
			,@MedicalSectionPageNumber	
			,@MedicalCardHolderName
			,@Designation
			,@PatientName
			,@Gender
			,@DateOfBirth
			,@Age
			,@ClaimFor
			,@RelationWithRetire
			,@MobileNumber
			,@CardCategory
			,@Address
			,@ClaimType
			,@MedicalCardPhotoCopy
			,@PrescriptionDetailPhotoCopy
			,@OriginalBill
			,@CashMemo
			,@ClaimStatusId
			,@CreatedBy)
	COMMIT TRAN  
	
	--SELECT @ClaimId = MAX(Id) FROM [dbo].[MediclaimNonCashless];
	 SET @ClaimId = SCOPE_IDENTITY();
	
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
GO
/****** Object:  StoredProcedure [dbo].[SaveMediclaimVoucher]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveMediclaimVoucher]
(
	@PayTo [NVARCHAR](30),
	@ApprovedAmount [MONEY],
	@AmountInWords [NVARCHAR](100),
	@EntryNo [INT],
	@PageNo [INT],
	@BankBranch [NVARCHAR](100),
	@AccountNumber[NVARCHAR](100),
	@BICCode [NVARCHAR](15),
	@IFSCCode [NVARCHAR](10),
	@CreatedBy [NVARCHAR](30),
	@VoucherId [INT] OUTPUT
)
AS
BEGIN TRAN
BEGIN TRY
	INSERT INTO dbo.MediclaimVoucher
	(PayTo, ApprovedAmount, AmountInWords, EntryNo, PageNo, BankBranch, AccountNumber, BICCode, IFSCCode, CreatedBy)
	 VALUES(@PayTo, @ApprovedAmount, @AmountInWords, @EntryNo, @PageNo,  @BankBranch,  @AccountNumber, @BICCode, @IFSCCode, @CreatedBy)
	 COMMIT TRAN
	 
	 SET @VoucherId = SCOPE_IDENTITY();
	 
END TRY
BEGIN CATCH
	ROLLBACK TRAN
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

GO
/****** Object:  StoredProcedure [dbo].[SaveOPDCND]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveOPDCND]
(	
	@OPDCNDDate date,
	@HospitalName nvarchar(40),
	@MedicineAmount money,
	@InvestigationAmount money,
	@ConsultationAmount money,
	@TotalAmount money,
	@HospitalizationClaimType nchar(3),
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30)
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[OPDCND]
           ([OPDCNDDate]
           ,[HospitalName]
           ,[MedicineAmount]
           ,[InvestigationAmount]
           ,[ConsultationAmount]
           ,[TotalAmount]
           ,[HospitalizationClaimType]
           ,[NonCashlessClaimId]
           ,[CreatedBy])
     VALUES
           (@OPDCNDDate
           ,@HospitalName
           ,@MedicineAmount
           ,@InvestigationAmount 
           ,@ConsultationAmount
           ,@TotalAmount
           ,@HospitalizationClaimType
           ,@NonCashlessClaimId
           ,@CreatedBy)
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
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEditBudgetDeclaration]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =======================            
-- Author:  Mita Makwana KodXL Technologies            
-- Create date: 30/10/2021            
-- Description: CURD Roles Page Access            
-- exec [sp_AddEditBudgetDeclaration]        
-- ==========================      
CREATE proc [dbo].[sp_AddEditBudgetDeclaration]  

@ID uniqueidentifier = null,
@FinancialYear as varchar(50),
@AllocationProgram as varchar(500),
@AllocatedFunds as decimal(18,2),
@DisbursementPeriod as varchar(500),
@DisbursementAuthority as varchar(500),
@CreatedBy as nvarchar(50)=null,
@ModifideBy as nvarchar(50)=null,
@ReturnMsg as nvarchar(max) output
as
begin
SET NOCOUNT ON
if(@ID is null)
begin
			insert into BudgetDeclaration (
					FinancialYear,
					AllocationProgram,
					AllocatedFunds,
					DisbursementPeriod,
					DisbursementAuthority,
					CreatedDate,
					CreatedBy,
					ModifyDate,
					ModifideBy) 
					values(
					@FinancialYear,
					@AllocationProgram,
					@AllocatedFunds,
					@DisbursementPeriod,
					@DisbursementAuthority,
					GETDATE(),
					@CreatedBy,
					Getdate(),
					@ModifideBy)
			set @ReturnMsg='add'

end
else
begin
		update BudgetDeclaration set
		
				
				FinancialYear=@FinancialYear,
				AllocationProgram=@AllocationProgram,
				AllocatedFunds=@AllocatedFunds,
				DisbursementPeriod=@DisbursementPeriod,
				DisbursementAuthority=@DisbursementAuthority,
				ModifyDate=GETDATE(),
				ModifideBy=@ModifideBy where ID=@ID
				set @ReturnMsg ='update'  		
end

end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEditEmployeeProfile]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_AddEditEmployeeProfile]    
  @Prefix nvarchar(max),    
  @Id uniqueIdentifier = null, 
  @ProfileImage varbinary(MAX),
        @EmployeeName nvarchar(max),    
        @EmployeeId bigInt,    
        @DOB date,    
  @Gender nvarchar(max),    
        @DTLOffice nvarchar(max),    
        @Designation nvarchar(max),    
        @ServiceStartDate date,    
        @ServiceEndDate date,    
        @RAddress nvarchar(max),    
        @PAddress nvarchar(max),    
        @RState nvarchar(max),    
        @PState nvarchar(max),    
        @RDistrict nvarchar(max),    
        @PDistrict nvarchar(max),    
        @RZipcode nvarchar(max),    
        @PZipcode nvarchar(max),    
        @EmailAddress nvarchar(max),    
        @Mobile nvarchar(max),    
        @Phone nvarchar(max),
		@ReasonOfRetirement nvarchar(max),
  @CreatedBy nvarchar(max) = null ,  
  @ModifideBy nvarchar(max) = null,  
  @ReturnMsg nvarchar(max) output  
AS    
BEGIN   
  
begin try  
   print @ProfileImage
if( @Id is null)  
begin  
 
 INSERT INTO [EmployeeRegistration]    
           ([Prefix]    
           ,[EmployeeName]    
           ,[EmployeeId]    
           ,[DOB]    
           ,[Gender]    
           ,[DTLOffice]    
           ,[Designation]    
           ,[ServiceStartDate]    
           ,[ServiceEndDate]    
           ,[ResidentialAddress]    
           ,[PermanentAddress]    
           ,[RState]    
           ,[RDistrict]    
           ,[RZipCode]    
           ,[PState]    
           ,[PDistrict]    
           ,[PZipCode]    
           ,[EmailAddress]    
           ,[Mobile]    
           ,[Phone]
		   ,[ReasonOfRetirement]
     ,[CreatedBy]
	 ,[ProfileImage])    
     VALUES    
           (@Prefix    
           ,@EmployeeName    
           ,@EmployeeId   
           ,@DOB    
           ,@Gender    
           ,@DTLOffice    
           ,@Designation    
           ,@ServiceStartDate    
           ,@ServiceEndDate    
           ,@RAddress    
           ,@PAddress    
           ,@RState    
           ,@RDistrict    
           ,@RZipcode    
           ,@PState    
           ,@PDistrict    
           ,@PZipcode    
           ,@EmailAddress    
           ,@Mobile    
           ,@Phone
		   ,@ReasonOfRetirement
     ,@CreatedBy
	 ,@ProfileImage)   
  set @ReturnMsg ='add'  
  end  
  else  
  begin  
    
  Update  [EmployeeRegistration]    
  set   
           [Prefix]  =@Prefix    
           ,[EmployeeName]  =@EmployeeName    
		   ,[ProfileImage] = @ProfileImage
           ,[DOB]  =@DOB  
           ,[Gender]  =@Gender   
           ,[DTLOffice]  =@DTLOffice    
           ,[Designation]  =@Designation    
           ,[ServiceStartDate]  =@ServiceStartDate    
           ,[ServiceEndDate]  =@ServiceEndDate    
           ,[ResidentialAddress] = @RAddress    
           ,[PermanentAddress]  =@PAddress  
           ,[RState]  =@RState    
           ,[RDistrict]  =@RDistrict    
           ,[RZipCode]  =@RZipcode  
           ,[PState]  =@PState    
           ,[PDistrict]  =@PDistrict   
           ,[PZipCode]  =@PZipcode    
           ,[EmailAddress]  =@EmailAddress   
           ,[Mobile]  =@Mobile   
           ,[Phone]  =@Phone  
		   ,[ReasonOfRetirement] = @ReasonOfRetirement
     ,ModifideBy = @ModifideBy  
     ,ModifideDate = GETDATE()  
     where Id = @Id 
        set @ReturnMsg ='update'  
  end  
  end try  
 begin catch   
  set @ReturnMsg = ERROR_MESSAGE()   
  
  end catch  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEditGrievance]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =======================            
-- Author:  Mita Makwana KodXL Technologies            
-- Create date: 11/9/2021            
-- Description: CURD Roles Page Access            
-- exec [sp_AddEditGrievance]        
-- ==========================      
CREATE proc [dbo].[sp_AddEditGrievance]  


@Id  uniqueidentifier = null,
@Department as varchar(100),
@PPONo as varchar(50),
@EmployeeId as bigint,
@Subject as varchar(500),
@MobileNo as varchar(100),
@GrievanceDetails as varchar(2000),
@CreatedBy as nvarchar(50)=null,
@ModifideBy as nvarchar(50)=null,
@ReturnMsg nvarchar(max) output

as  
begin  
SET NOCOUNT ON 


if(@Id is null)
begin

insert into Grievance(
						Department,
						PPONo,
						EmployeeId,
						Subject,
						MobileNo,
						GrievanceDetails,
						CreatedDate,
						CreatedBy)
						values(
						@Department,
						@PPONo,
						@EmployeeId,
						@Subject,
						@MobileNo,
						@GrievanceDetails,
						GETDATE(),
						@CreatedBy)
						set @ReturnMsg ='add'  
end
else
begin

update Grievance  set 

				Department=@Department,
				PPONo=@PPONo,
				EmployeeId=@EmployeeId,
				Subject=@Subject,
				MobileNo=@MobileNo,
				GrievanceDetails=@GrievanceDetails,
				ModifideDate=Getdate(),
				ModifideBy=@ModifideBy

where Id = @Id
set @ReturnMsg ='update'  
end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEditInvestmentDeclaration]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =======================            
-- Author:  Mita Makwana KodXL Technologies            
-- Create date: 11/17/2021            
-- Description: CURD Roles Page Access            
-- exec [sp_AddEditTDSInvestment]        
-- ==========================      
CREATE proc [dbo].[sp_AddEditInvestmentDeclaration]  


@Id  uniqueidentifier = null,
@FinancialYear varchar(50),
@InvestmentType varchar(100),
@InvestmentTitle varchar(500),
@ReferenceNo varchar(100),
@InvestedAmount decimal(18,2),
@ClosingPeriod varchar(50),
@FromDate date,
@ToDate date,
@ExpectedProfitMargin decimal(18,2),
@ExpectedROI decimal(18,2),
--@ActualReturnOnInvestment decimal(18,2),
--@InvestmentClose bit,
@CreatedBy as nvarchar(50)=null,
@ModifideBy as nvarchar(50)=null,
@ReturnMsg nvarchar(max) output

as  
begin  
SET NOCOUNT ON 


if(@Id is null)
begin

insert into InvestmentDeclaration(FinancialYear,
InvestmentType,
InvestmentTitle,
ReferenceNo,
InvestedAmount,
ClosingPeriod,
FromDate,
ToDate,
ExpectedProfitMargin,
ExpectedROI,
--ActualReturnOnInvestment,
--InvestmentClose,
CreatedDate,
CreatedBy)
values(@FinancialYear,
@InvestmentType,
@InvestmentTitle,
@ReferenceNo,
@InvestedAmount,
@ClosingPeriod,
@FromDate,
@ToDate,
@ExpectedProfitMargin,
@ExpectedROI,
--@ActualReturnOnInvestment,
--@InvestmentClose,
Getdate(),@CreatedBy)
set @ReturnMsg ='add'  
end
else
begin

update InvestmentDeclaration  set 
FinancialYear=@FinancialYear,
InvestmentType=@InvestmentType,
InvestmentTitle=@InvestmentTitle,
ReferenceNo=@ReferenceNo,
InvestedAmount=@InvestedAmount,
ClosingPeriod=@ClosingPeriod,
FromDate=@FromDate,
ToDate=@ToDate,
ExpectedProfitMargin=@ExpectedProfitMargin,
ExpectedROI=@ExpectedROI,
--ActualReturnOnInvestment=@ActualReturnOnInvestment,
--InvestmentClose=@InvestmentClose,
ModifideDate=Getdate(),
ModifideBy=@ModifideBy

where Id = @Id
set @ReturnMsg ='update'  
end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEditRolePageAccess]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =======================            
-- Author:  Darshan Dave KodXL Technologies            
-- Create date: 10/8/2021            
-- Description: CURD Roles Page Access            
-- exec [sp_AddEditRolePageAccess]        
-- ==========================      
CREATE proc [dbo].[sp_AddEditRolePageAccess]   
@id int = 0,
@roleId nvarchar(max)  ,
@pageName nvarchar(max) =null,
@flag int = 0
as  
begin  
SET NOCOUNT ON 
declare @tempId int  
declare @tempRoleId nvarchar(max)  
declare @tempPageName nvarchar(max)  
declare @tempFlag int

set @tempId = @id  
set @tempRoleId = @roleId  
set @tempPageName = @pageName  
set @tempFlag = @flag  
 
if(@tempFlag =0) -- select All
begin
select rp.*,r.Name as RoleName from RolePages as rp
left join AspNetRoles as r on rp.RoleId = r.Id
end
else if(@tempFlag =1) -- select by id
begin
select rp.*,r.Name as RoleName from RolePages as rp
left join AspNetRoles as r on rp.RoleId = r.Id where rp.id = @tempId
end
else if(@tempFlag = 2) -- insert data
begin

declare @tempExist nvarchar(max) 

select @tempExist = RoleId from RolePages where RoleId = @tempRoleId

if @tempExist is  null
begin
insert into RolePages(RoleId,PageName) values(@tempRoleId,@tempPageName)
end 
else 
begin
return 0
end
end
else if(@tempFlag =3) -- update data 
begin
update RolePages set PageName = @tempPageName where RoleId = @tempRoleId
end if(@tempFlag = 4) -- delete data
begin
delete RolePages where Id = @tempId
end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEditTDSCalculation]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =======================            
-- Author:  Mita Makwana KodXL Technologies            
-- Create date: 11/9/2021            
-- Description: CURD Roles Page Access            
-- exec [sp_AddEditTDSCalculation]        
-- ==========================      
CREATE proc [dbo].[sp_AddEditTDSCalculation]  


@ID  uniqueidentifier = null,
@EmployeeRegistrationId uniqueidentifier,
@FinancialYear int,
@Pensioner as varchar(500),
@Pension as decimal (18,2) ,
@Gratuity as decimal (18,2),
@Commutation as decimal (18,2),
@LeaveEncashment as decimal(18,2),
@AQP as decimal (18,2),
@DA as decimal (18,2),
@HRA as decimal (18,2),
@Travel_Allowance as decimal(18,2),
@Medical_Allowance as decimal(18,2),
@OtherIncome as decimal (18,2),
@ExemptedAmount as decimal (18,2),
@Ex80DD  as decimal (18,2),
@Ex80C  as decimal (18,2),
@Mediclaim as decimal (18,2),
@HomeLoan as decimal (18,2),
@IntOnEduLoan as decimal (18,2),
@Donation as decimal (18,2),
@TaxableAmount as decimal (18,2),
@StandardDeduction as decimal (18,2),
@NetTaxableAmount as decimal (18,2),
@TaxPayable as decimal (18,2),
@Cess as decimal(18,2),
@MonthlyTDSAmount as decimal (18,2),
@Regime as varchar(10),
@CreatedBy as nvarchar(50)=null,
@ModifideBy as nvarchar(50)=null,
@ReturnId uniqueidentifier output
--@ReturnMsg nvarchar(max) output

as  
begin  
SET NOCOUNT ON 


if(@Id is null)
begin
set @Id=newid()
insert into TDSCalculation(ID,EmployeeRegistrationId,FinancialYear,Pensioner,Pension,Gratuity,Commutation,LeaveEncashment,AQP,DA,HRA,Travel_Allowance,Medical_Allowance,OtherIncome,ExemptedAmount,Ex80DD,
Ex80C,Mediclaim,HomeLoan,IntOnEduLoan,Donation,TaxableAmount,StandardDeduction,NetTaxableAmount,TaxPayable,Cess,MonthlyTDSAmount,Regime,
CreatedDate,CreatedBy)
values(@Id,@EmployeeRegistrationId,@FinancialYear,@Pensioner,@Pension,@Gratuity,@Commutation,@LeaveEncashment,@AQP,@DA,@HRA,@Travel_Allowance,@Medical_Allowance,@OtherIncome,@ExemptedAmount,@Ex80DD,
@Ex80C,@Mediclaim,@HomeLoan,@IntOnEduLoan,@Donation,@TaxableAmount,@StandardDeduction,@NetTaxableAmount,@TaxPayable,@Cess,@MonthlyTDSAmount,@Regime,
GETDATE(),@CreatedBy)
--set @ReturnMsg ='add'  
set @ReturnId=@Id

end
else
begin

update TDSCalculation  set 

EmployeeRegistrationId=@EmployeeRegistrationId,
FinancialYear=@FinancialYear,
Pensioner=@Pensioner,
Pension=@Pension,
Gratuity=@Gratuity,
Commutation=@Commutation,
LeaveEncashment=@LeaveEncashment,
AQP=@AQP,
DA=@DA,
HRA=@HRA,
Travel_Allowance=@Travel_Allowance,
Medical_Allowance=@Medical_Allowance,
OtherIncome=@OtherIncome,
ExemptedAmount=@ExemptedAmount,
Ex80DD=@Ex80DD,
Ex80C=@Ex80C,
Mediclaim=@Mediclaim,
HomeLoan=@HomeLoan,
IntOnEduLoan=@IntOnEduLoan,
Donation=@Donation,
TaxableAmount=@TaxableAmount,
StandardDeduction=@StandardDeduction,
NetTaxableAmount=@NetTaxableAmount,
TaxPayable=@TaxPayable,
Cess=@Cess,
MonthlyTDSAmount=@MonthlyTDSAmount,
Regime=@Regime,
ModifyDate=Getdate(),
ModifideBy=@ModifideBy

where ID = @Id 
--set @ReturnMsg ='update'  
set @ReturnId=@ID
end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddEditTDSInvestment]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =======================            
-- Author:  Mita Makwana KodXL Technologies            
-- Create date: 11/9/2021            
-- Description: CURD Roles Page Access            
-- exec [sp_AddEditTDSInvestment]        
-- ==========================      
CREATE proc [dbo].[sp_AddEditTDSInvestment]  


@TDSInvestmentID  uniqueidentifier = null,
@TDSCalculationId uniqueidentifier,
@Inv80D as decimal(18,2),
@InvFile80D as varbinary(max),
@Inv80DD as decimal(18,2),
@InvFile80DD as varbinary(max),
@Inv80DDB as decimal(18,2),
@InvFile80DDB as varbinary(max),
@Inv5YrsTermDepositPostoffice as decimal(18,2),
@InvFile5YrsTermDepositPostoffice as varbinary(max),
@InvLIC_Pension_Plan as decimal(18,2),
@InvFileLIC_Pension_Plan as varbinary(max),
@InvNSC as decimal(18,2),
@InvFileNSC as varbinary(max),
@InvPPF  as decimal(18,2),
@InvFilePPF as varbinary(max),
@InvStampDuty as decimal(18,2),
@InvFileStampDuty as varbinary(max),
@InvSukanyaSmriddhiYojana as decimal(18,2),
@InvFileSukanyaSmriddhiYojana as varbinary(max),
@InvTermDepositBank as decimal(18,2),
@InvFileTermDepositBank as varbinary(max),
@InvTF as decimal(18,2),
@InvFileTF as varbinary(max),
@InvULIP as decimal(18,2),
@InvFileULIP  as varbinary(max),
@InvMF  as decimal(18,2),
@InvFileMF as varbinary(max),
@InvHousingLoanInt as decimal(18,2),
@InvFileHousingLoanInt as varbinary(max),
@InvHousingLoanInt1617 as decimal(18,2),
@InvFileHousingLoanInt1617 as varbinary(max),
@InvHousingLoanInt1920 as decimal(18,2),
@InvFileHousingLoanInt1920 as varbinary(max),
@Inv80E as decimal(18,2),
@InvFile80E as varbinary(max),
@Inv80G as decimal(18,2),
@InvFile80G as varbinary(max),
@Inv80GGB as decimal(18,2),
@InvFile80GGB as varbinary(max),
@Inv80GGC as decimal(18,2),
@InvFile80GGC as varbinary(max),
@Inv80GG as decimal(18,2),
@InvFile80GG as varbinary(max),
@Inv80RRB as decimal(18,2),
@InvFile80RRB as varbinary(max),
@Inv80TTA as decimal(18,2),
@InvFile80TTA as varbinary(max),
@Inv80TTB as decimal(18,2),
@InvFile80TTB as varbinary(max),
@Inv80U as decimal(18,2),
@InvFile80U as varbinary(max),
@CreatedBy as nvarchar(50)=null,
@ModifideBy as nvarchar(50)=null,
@ReturnMsg nvarchar(max) output

as  
begin  
SET NOCOUNT ON 


if(@TDSInvestmentID is null)
begin

insert into TDSInvestment(
TDSCalculationId,Inv80D,InvFile80D,Inv80DD,InvFile80DD,Inv80DDB,InvFile80DDB,Inv5YrsTermDepositPostoffice,InvFile5YrsTermDepositPostoffice,
InvLIC_Pension_Plan,InvFileLIC_Pension_Plan,InvNSC,InvFileNSC,InvPPF,InvFilePPF,InvStampDuty,InvFileStampDuty,InvSukanyaSmriddhiYojana,
InvFileSukanyaSmriddhiYojana,InvTermDepositBank,InvFileTermDepositBank,InvTF,InvFileTF,InvULIP,InvFileULIP,InvMF,InvFileMF,InvHousingLoanInt,
InvFileHousingLoanInt,InvHousingLoanInt1617,InvFileHousingLoanInt1617,InvHousingLoanInt1920,InvFileHousingLoanInt1920,Inv80E,InvFile80E,
Inv80G,InvFile80G,Inv80GGB,InvFile80GGB,Inv80GGC,InvFile80GGC,Inv80GG,InvFile80GG,Inv80RRB,InvFile80RRB,
Inv80TTA,InvFile80TTA,Inv80TTB,InvFile80TTB,Inv80U,InvFile80U,CreatedDate,CreatedBy)
values(
@TDSCalculationId,@Inv80D,@InvFile80D,@Inv80DD,@InvFile80DD,@Inv80DDB,@InvFile80DDB,@Inv5YrsTermDepositPostoffice,@InvFile5YrsTermDepositPostoffice,
@InvLIC_Pension_Plan,@InvFileLIC_Pension_Plan,@InvNSC,@InvFileNSC,@InvPPF,@InvFilePPF,@InvStampDuty,@InvFileStampDuty,@InvSukanyaSmriddhiYojana,
@InvFileSukanyaSmriddhiYojana,@InvTermDepositBank,@InvFileTermDepositBank,@InvTF,@InvFileTF,@InvULIP,@InvFileULIP,@InvMF,@InvFileMF,@InvHousingLoanInt,
@InvFileHousingLoanInt,@InvHousingLoanInt1617,@InvFileHousingLoanInt1617,@InvHousingLoanInt1920,@InvFileHousingLoanInt1920,@Inv80E,@InvFile80E,
@Inv80G,@InvFile80G,@Inv80GGB,@InvFile80GGB,@Inv80GGC,@InvFile80GGC,@Inv80GG,@InvFile80GG,@Inv80RRB,@InvFile80RRB,
@Inv80TTA,@InvFile80TTA,@Inv80TTB,@InvFile80TTB,@Inv80U,@InvFile80U,GETDATE(),@CreatedBy)
set @ReturnMsg ='add'  
end
else
begin

update TDSInvestment  set 

TDSCalculationId=@TDSCalculationId,
Inv80D=@Inv80D,
InvFile80D=@InvFile80D,
Inv80DD=@Inv80DD,
InvFile80DD=@InvFile80DD,
Inv80DDB=@Inv80DDB,
InvFile80DDB=@InvFile80DDB,
Inv5YrsTermDepositPostoffice=@Inv5YrsTermDepositPostoffice,
InvFile5YrsTermDepositPostoffice=@InvFile5YrsTermDepositPostoffice,
InvLIC_Pension_Plan=@InvLIC_Pension_Plan,
InvFileLIC_Pension_Plan=@InvFileLIC_Pension_Plan,
InvNSC=@InvNSC,
InvFileNSC=@InvFileNSC,
InvPPF=@InvPPF,
InvFilePPF=@InvFilePPF,
InvStampDuty=@InvStampDuty,
InvFileStampDuty=@InvFileStampDuty,
InvSukanyaSmriddhiYojana=@InvSukanyaSmriddhiYojana,
InvFileSukanyaSmriddhiYojana=@InvFileSukanyaSmriddhiYojana,
InvTermDepositBank=@InvTermDepositBank,
InvFileTermDepositBank=@InvFileTermDepositBank,
InvTF=@InvTF,
InvFileTF=@InvFileTF,
InvULIP=@InvULIP,
InvFileULIP=@InvFileULIP,
InvMF=@InvMF,
InvFileMF=@InvFileMF,
InvHousingLoanInt=@InvHousingLoanInt,
InvFileHousingLoanInt=@InvFileHousingLoanInt,
InvHousingLoanInt1617=@InvHousingLoanInt1617,
InvFileHousingLoanInt1617=@InvFileHousingLoanInt1617,
InvHousingLoanInt1920=@InvHousingLoanInt1920,
InvFileHousingLoanInt1920=@InvFileHousingLoanInt1920,
Inv80E=@Inv80E,
InvFile80E=@InvFile80E,
Inv80G=@Inv80G,
InvFile80G=@InvFile80G,
Inv80GGB=@Inv80GGB,
InvFile80GGB=@InvFile80GGB,
Inv80GGC=@Inv80GGC,
InvFile80GGC=@InvFile80GGC,
Inv80GG=@Inv80GG,
InvFile80GG=@InvFile80GG,
Inv80RRB=@Inv80RRB,
InvFile80RRB=@InvFile80RRB,
Inv80TTA=@Inv80TTA,
InvFile80TTA=@InvFile80TTA,
Inv80TTB=@Inv80TTB,
InvFile80TTB=@InvFile80TTB,
Inv80U=@Inv80U,
InvFile80U=@InvFile80U,
ModifideDate=Getdate(),
ModifideBy=@ModifideBy

where TDSInvestmentID = @TDSInvestmentID
set @ReturnMsg ='update'  
end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddForm5]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE   PROCEDURE [dbo].[sp_AddForm5]          
   @Id uniqueIdentifier ,        
           @Aadhar nvarchar(max),          
           @PAN nvarchar(max),          
           @BankAccountNumber nvarchar(max),
           @Bank nvarchar(max),   
		   @BankAccountName nvarchar(max),
           @BankAddress nvarchar(max),          
           @IFSCCode nvarchar(max),          
           @BICCode nvarchar(max),          
           @IdentificationMark nvarchar(max),          
           @PanCard varbinary(max) = null,          
           @AadharCard varbinary(max) = null,          
           @SpecimenSignature1 image =null ,        
           @SpecimenSignature2 image =null ,        
           @SpecimenSignature3 image =null ,        
           @ThumbImpression1 image =null ,        
           @ThumbImpression2 image =null ,        
     @EmployeeRegistrationId uniqueIdentifier        
AS          
BEGIN          
if(@id is null)        
begin        
 INSERT INTO [dbo].[Form5]          
           ([Aadhar]          
           ,[PAN]   
		   ,[BankAccountNumber]
           ,[Bank] 
		   ,[BankAccountName]   		    
           ,[BankAddress]          
           ,[IFSCCode]          
           ,[BICCode]          
           ,[IdentificationMark]          
           ,[PanCard]          
           ,[AadharCard]          
           ,[SpecimenSignature]  
           ,[SpecimenSignature2]  
           ,[SpecimenSignature3]  
           ,[ThumbImpression1]  
           ,[ThumbImpression2]  
     ,[EmployeeRegistrationId])          
     VALUES          
           (          
           @Aadhar,          
           @PAN, 
		   @BankAccountNumber,
           @Bank,  
		   @BankAccountName,    
           @BankAddress,          
           @IFSCCode,          
           @BICCode,          
           @IdentificationMark,          
           @PanCard,          
           @AadharCard,          
           @SpecimenSignature1,        
           @SpecimenSignature2,        
           @SpecimenSignature3,        
           @ThumbImpression1,        
           @ThumbImpression2,        
     @EmployeeRegistrationId)          
END          
else        
begin        
        
update Form5 set        
[Aadhar]  =@Aadhar        
           ,[PAN] =@PAN     
		   ,[BankAccountNumber] = @BankAccountNumber
           ,[Bank]  =@Bank  
		   ,[BankAccountName] = @BankAccountName 
           ,[BankAddress] =@BankAddress         
           ,[IFSCCode]  =@IFSCCode        
           ,[BICCode]  = @BICCode        
           ,[IdentificationMark] =@IdentificationMark         
           ,[PanCard]  =case when @PanCard is not null   then  @PanCard else PanCard end    
           ,[AadharCard]  =case when @AadharCard  is not null then @AadharCard else AadharCard end       
           ,[SpecimenSignature] = case when @SpecimenSignature1  is not null then @SpecimenSignature1 else SpecimenSignature end            
     ,[SpecimenSignature2] = case when @SpecimenSignature2  is not null then @SpecimenSignature2 else SpecimenSignature2 end            
     ,[SpecimenSignature3] = case when @SpecimenSignature3  is not null then @SpecimenSignature3 else SpecimenSignature3 end            
     ,[ThumbImpression1] = case when @ThumbImpression1  is not null then @ThumbImpression1 else ThumbImpression1 end            
     ,[ThumbImpression2] = case when @ThumbImpression2  is not null then @ThumbImpression2 else ThumbImpression2 end            
     where ID= @Id and EmployeeRegistrationId = @EmployeeRegistrationId        
        
end        
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AddNomineeDetails]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddNomineeDetails]  
@Id uniqueIdentifier = null,  
      @MemberName nvarchar(max),  
   @RelationShip nvarchar(max),  
   @DateOfBirth date,  
   --@ContigencyReason nvarchar(max),  
   @GuardianName nvarchar(max),
   @GuardianRelation varchar(200),
   @GuardianAddress varchar(max),
   @Commutation int,  
   @Arrear int,  
   @EmployeeRegistrationId uniqueidentifier  
AS  
BEGIN  


INSERT INTO [dbo].[NomineeDetails]  
           ([MemberName]  
           ,[RelationShip]  
           ,[DateOfBirth]  
           --,[ContigencyReason]  
           ,[GuardianName]
		   ,[GuardianRelation]
		   ,[GuardianAddress]
           ,[Commutation]  
           ,[Arrear]  
     ,[EmployeeRegistrationId]  
           )  
     VALUES  
           (  
           @MemberName,  
           @RelationShip,  
           @DateOfBirth,  
           --@ContigencyReason,  
           @GuardianName, 
		   @GuardianRelation,
		   @GuardianAddress,
           @Commutation,  
           @Arrear,  
     @EmployeeRegistrationId)  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AddPensionSlip]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_AddPensionSlip]    
@Id uniqueidentifier = null,  
@ABPLast10Months int, 
@AdmissiablePension DECIMAL(18,6),
@EmolumentsForPension int,   
@AdmissiableDate date,  
@PensionEnhancedRate int,   
@AdmissibleForFromDate_Enhanced date,   
@AdmissibleForToDate_Enhanced date,  
@PensionAtNormalRate int,  
@AdmissibleForFromDate_Normal date,   
@AdmissibleForToDate_Normal date,  
@Commutation int,  
@CommutedPortion int,   
@GratuityType nvarchar(max),   
@Gratuity int,   
@CreatedBy nvarchar(max) = null,  
@EmployeeRegistrationId uniqueidentifier,
@LeaveEncashment decimal(18,2),
@Taxable decimal(18,2),
@NonTaxable decimal(18,2),
@LeaveEncashmentDays int,
@LumsumCommu decimal(18,2),
@TaxableCommu decimal(18,2),
@ServicePeriod varchar(500),
@TaxableLeaveEncashment decimal(18,2),
@ModifideBy nvarchar(max) = null,  
@ReturnMsg nvarchar(max) output  
as    
begin    
SET NOCOUNT ON   
  
  
if(@Id is null)  
begin  
insert into PensionSlip( ABPLast10Months, AdmissiablePension,EmolumentsForPension, AdmissiableDate, PensionEnhancedRate,  
AdmissibleForFromDate_Enhanced, AdmissibleForToDate_Enhanced, PensionAtNormalRate, AdmissibleForFromDate_Normal,   
AdmissibleForToDate_Normal, Commutation, CommutedPortion, GratuityType, Gratuity, CreatedDate, CreatedBy,EmployeeRegistrationId,LeaveEncashment,Taxable,NonTaxable
,LeaveEncashmentDays,LumsumCommu,TaxableCommu,ServicePeriod,TaxableLeaveEncashment)  
values  
(@ABPLast10Months,@AdmissiablePension,@EmolumentsForPension,@AdmissiableDate,@PensionEnhancedRate,  
@AdmissibleForFromDate_Enhanced,@AdmissibleForToDate_Enhanced,@PensionAtNormalRate,@AdmissibleForFromDate_Normal,  
@AdmissibleForToDate_Normal,@Commutation,@CommutedPortion,@GratuityType,@Gratuity,GETDATE(),@CreatedBy,@EmployeeRegistrationId
,@LeaveEncashment,@Taxable,@NonTaxable,@LeaveEncashment,@LumsumCommu,@TaxableCommu,@ServicePeriod,@TaxableLeaveEncashment)  
end  
else  
begin  
  
update PensionSlip set   
ABPLast10Months =@ABPLast10Months,  
AdmissiablePension=@AdmissiablePension,
EmolumentsForPension = @EmolumentsForPension,  
AdmissiableDate = @AdmissiableDate,  
PensionEnhancedRate = @PensionEnhancedRate,  
AdmissibleForFromDate_Enhanced = @AdmissibleForFromDate_Enhanced,  
AdmissibleForToDate_Enhanced = @AdmissibleForToDate_Enhanced,  
PensionAtNormalRate = @PensionAtNormalRate,  
AdmissibleForFromDate_Normal = @AdmissibleForFromDate_Normal,   
AdmissibleForToDate_Normal = @AdmissibleForToDate_Normal,  
Commutation = @Commutation,  
CommutedPortion = @CommutedPortion,   
GratuityType = @Gratuity,  
Gratuity = @Gratuity,
LeaveEncashment = @LeaveEncashment,
Taxable = @Taxable,
NonTaxable = @NonTaxable,
LeaveEncashmentDays = @LeaveEncashmentDays,
LumsumCommu = @LumsumCommu,
TaxableCommu = @TaxableCommu,
ServicePeriod=@ServicePeriod,
TaxableLeaveEncashment=@TaxableLeaveEncashment,
ModifideBy = @ModifideBy,  
ModifideDate = GETDATE()  
  
where ID = @Id and EmployeeRegistrationId = @EmployeeRegistrationId  
  
end  
end
GO
/****** Object:  StoredProcedure [dbo].[sp_CloseInvestment]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =======================            
-- Author:  Mita Makwana KodXL Technologies            
-- Create date: 11/9/2021            
-- Description: CURD Roles Page Access            
-- exec [sp_UpdateEmployeeRegStatusByHRAdmin]        
-- ==========================      
create proc [dbo].[sp_CloseInvestment]  



@InvestmentId uniqueidentifier,
@ActualReturnOnInvestment decimal(18,2)
as  
begin  
SET NOCOUNT ON 
update InvestmentDeclaration set ActualReturnOnInvestment=@ActualReturnOnInvestment,InvestmentClose=1
where Id=@InvestmentId 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteBudgetByFinancialYear]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE proc [dbo].[sp_DeleteBudgetByFinancialYear]     
@FinancialYear varchar(50)
as    
begin    
  SET NOCOUNT ON   
  delete from BudgetDeclaration where FinancialYear=@FinancialYear  
  end  
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteEmp]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE proc [dbo].[sp_DeleteEmp]       
@EmployeeRegistrationId uniqueidentifier ,
@DeleteReason nvarchar(max)
  
as      
begin      
 if(EXISTS (select ID from EmployeeRegistration where Id = @EmployeeRegistrationId))
 begin
 update EmployeeRegistration set DeleteReason = @DeleteReason,ISDelete=1 where Id = @EmployeeRegistrationId
 end
end    

GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteGrievance]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
create proc [dbo].[sp_DeleteGrievance]     
@Id uniqueidentifier

as    
begin    
  SET NOCOUNT ON   
  
  delete from Grievance where id=@Id
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteNonimees]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_DeleteNonimees]
@EmployeeRegistrationId uniqueidentifier
as
begin


if(EXISTS (select ID from NomineeDetails where EmployeeRegistrationId = @EmployeeRegistrationId))  
begin  
delete NomineeDetails where EmployeeRegistrationId = @EmployeeRegistrationId
end

end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetBudgetByFinancialYear]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE proc [dbo].[sp_GetBudgetByFinancialYear]     
@FinancialYear varchar(50)
as    
begin    
  SET NOCOUNT ON   
  Select * from BudgetDeclaration where FinancialYear=@FinancialYear  
  end  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployee]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

        
-- =======================                  
-- Author:  Darshan Dave KodXL Technologies                  
-- Create date: 10/8/2021                  
-- Description: Get Role Pages list                  
-- exec [sp_GetEmployee]    null,'Admin'          
-- ==========================            
CREATE proc [dbo].[sp_GetEmployee]         
@employeeId uniqueIdentifier,    
@role nvarchar(max) =null    
as        
begin        
  SET NOCOUNT ON       
declare @tempEmployeeId uniqueIdentifier      
set @tempEmployeeId = @employeeId      
      
if @tempEmployeeId is null      
begin      
select er.*
from EmployeeRegistration as er    
where IsDelete = 0
--left join PPOStatus as pps on pps.Id = er.PPOStatusId     
    
--where pps.StatusName in ('Accepted')    
--(case when @role = 'Employer' then 'Created' else    
--      case when @role = 'HOD' then 'Submitted' else    
--      case when @role = 'Admin' then 'Approved' else    
--      case when @role = 'Admin' then 'Accepted'     
--      else 'Created'  end end end end)    
    
    
end      
else      
begin      
select er.*,pps.StatusName as PPOStatusName, pps.Id as PPOStatusId,frm.*,nd.*,sh.*,ps.* from EmployeeRegistration as er       
left join PPOStatus as pps on pps.Id = er.PPOStatusId      
left join Form5 as frm on er.ID = frm.EmployeeRegistrationId      
left join NomineeDetails as nd on er.ID = nd.EmployeeRegistrationId      
left join ServiceHistory as sh on er.ID = sh.EmployeeRegistrationId      
left join PensionSlip as ps on er.ID = ps.EmployeeRegistrationId      
where er.ID = @tempEmployeeId   and IsDelete = 0   
end      
end 

GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployeeDetailsByDTLOffice]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_GetEmployeeDetailsByDTLOffice]   
@DTLOffice as varchar(500)
as  
begin  
  SET NOCOUNT ON 
  if @DTLOffice!=''
  select *,DATEDIFF(hour,DOB,GETDATE())/8766 AS Age from EmployeeRegistration  where DTLOffice = @DTLOffice 
else
Select *,DATEDIFF(hour,DOB,GETDATE())/8766 AS Age from EmployeeRegistration
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployeeDetailsById]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_GetEmployeeDetailsById]
@employeeId uniqueIdentifier
as
begin
SET NOCOUNT ON
select * from Form5 where EmployeeRegistrationId = @employeeId
select * from NomineeDetails where EmployeeRegistrationId = @employeeId
select PS.*,ER.ServiceStartDate,ER.ServiceEndDate,ER.DOB from PensionSlip PS JOIN [dbo].[EmployeeRegistration] ER ON PS.EmployeeRegistrationId = ER.ID where PS.EmployeeRegistrationId = @employeeId
select * from ServiceHistory where EmployeeRegistrationId = @employeeId
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetFinancialYearWisePensionDetails]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_GetFinancialYearWisePensionDetails]   
@EmpRegId uniqueIdentifier,
@FinYear int
as  
begin  
  SET NOCOUNT ON 
  if ( @FinYear!=0)
 /*Select Sum(AdmissiablePension)as Pension,sum(Commutation)as Commutation,Sum(Gratuity)as Gratuity,sum(Da) as DA,Sum(aqp.MonthlyAQPension)as AQP   from PensionSlip ps,ServiceHistory sh,
 AdditionalQuantamPay aqp

 where  ps.EmployeeRegistrationId=sh.EmployeeRegistrationId and ps.EmployeeRegistrationId=aqp.EmployeeRegistrationId 
 and  ps.EmployeeRegistrationId=@EmpRegId
 and YEAR(AdmissiableDate)=@FinYear
 and @FinYear between year(aqp.ToDate) and year(aqp.FromDate)*/


 
 declare @pension decimal(18,2)=0
 declare @commutation decimal(18,2)=0
 declare @gratuity decimal(18,2)=0
 declare @da decimal(18,2)=0
 declare @aqp decimal(18,2)=0
 declare @leaveencashment decimal(18,2)=0

 if exists(Select 1 from PensionSlip where EmployeeRegistrationId=@EmpRegId and year(AdmissiableDate)=@FinYear)
 begin
		 Select @pension=Sum(AdmissiablePension),@commutation=sum(LumsumCommu),@gratuity=Sum(Gratuity),@leaveencashment=sum(LeaveEncashment)   from PensionSlip ps
 		 where 
		 ps.EmployeeRegistrationId=@EmpRegId
		 and YEAR(AdmissiableDate)=@FinYear
 end
 else
 begin
		Select @pension=AdmissiablePension,@commutation=LumsumCommu,@gratuity=Gratuity,@leaveencashment=LeaveEncashment  from PensionSlip ps where ps.EmployeeRegistrationId=@EmpRegId
 end
 select @da=sum(da)from ServiceHistory where EmployeeRegistrationId=@EmpRegId
 
 
 Select @aqp =MonthlyAQPension from AdditionalQuantamPay aqp where EmployeeRegistrationId=@EmpRegId and @FinYear between year(aqp.ToDate) and year(aqp.FromDate)
 
 Select @pension as Pension,@commutation as commutation,@gratuity as gratuity,@da as DA,@aqp as AQP,@leaveencashment as LeaveEncashment
 
 

 
end


  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetGrievance]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
Create proc [dbo].[sp_GetGrievance]     

as    
begin    
  SET NOCOUNT ON   
Select * from Grievance
  end  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetGrievanceById]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
create proc [dbo].[sp_GetGrievanceById]     
@Id uniqueIdentifier
as    
begin    
  SET NOCOUNT ON   
Select * from Grievance where Id=@Id
  end  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetInvestmentDeclaration]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_GetInvestmentDeclaration]
@Financialyear varchar(50)=null,
@ReferenceNo varchar(50)=null,
@InvestmentType varchar(50)=null


as
begin
SET NOCOUNT ON
Select *,Datename(MM,FromDate)+'-'+cast(DATEPART(yyyy,FromDate)as varchar)as FromMonthYr,Datename(MM,ToDate)+'-'+cast(DATEPART(yyyy,ToDate)as varchar) as ToMonthYr from InvestmentDeclaration where (@FinancialYear is null or FinancialYear=@Financialyear) 
and  (@ReferenceNo is null or ReferenceNo=@ReferenceNo)
and (@InvestmentType is null or InvestmentType=@InvestmentType)
end

GO
/****** Object:  StoredProcedure [dbo].[sp_GetInvestmentDetailsByInvestmentId]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[sp_GetInvestmentDetailsByInvestmentId]
@InvestmentId uniqueIdentifier
as
begin
SET NOCOUNT ON
Select * from InvestmentDeclaration where Id=@InvestmentId
end

GO
/****** Object:  StoredProcedure [dbo].[sp_GetTDSCalculationDetails]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE proc [dbo].[sp_GetTDSCalculationDetails]     
@EmpRegId uniqueIdentifier,  
@FinYear int  
as    
begin    
  SET NOCOUNT ON   
  if ( @FinYear!=0)  
  Select * from EmployeeRegistration as emp
  left join TDSCalculation as tds on tds.EmployeeRegistrationId = emp.ID
  where emp.ID=@EmpRegId and tds.FinancialYear=@FinYear  
  end  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTDSInvestmentDetails]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE proc [dbo].[sp_GetTDSInvestmentDetails]     
@TDSCalculationId uniqueIdentifier
as    
begin    
  SET NOCOUNT ON   
  Select * from TDSInvestment where TDSCalculationId=@TDSCalculationId  
  end  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserRolePages]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
-- =======================            
-- Author:  Darshan Dave KodXL Technologies            
-- Create date: 10/8/2021            
-- Description: Get Role Pages list            
-- exec [sp_GetUserRolePages]        
-- ==========================      
CREATE proc [dbo].[sp_GetUserRolePages]   
@userId nvarchar(max) =null ,
@rolePageId int = null
as  
begin  
  SET NOCOUNT ON 
declare @tempUserId nvarchar(max)  
declare @tempRoleId int  
declare @tempRolePageId int
set @tempUserId = @userId  
set @tempRolePageId = @rolePageId  
  
if @tempUserId is not null  
begin  
select @tempRoleId = RoleId from AspNetUserRoles where UserId = @tempUserId   
  
select rp.Id,r.Name as RoleName,rp.RoleId,rp.PageName 
from RolePages as rp
left join AspNetRoles as r on rp.RoleId = r.Id 
where RoleId = @tempRoleId  
end  
else  
begin  

if @tempRolePageId is not null
begin
select rp.Id,r.Name as RoleName,rp.RoleId,rp.PageName 
from RolePages as rp
left join AspNetRoles as r on rp.RoleId = r.Id

end
else
begin
select rp.Id,r.Name as RoleName,rp.RoleId,rp.PageName 
from RolePages as rp
left join AspNetRoles as r on rp.RoleId = r.Id
where rp.Id = @tempRolePageId
end
  
end  
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ServiceHistory]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_ServiceHistory]      
@Id uniqueidentifier = null,      
     @DTLDepartmentName nvarchar(max),      
          -- @CategoryOfEmployeement nvarchar(max),      
           @ReasonOfRetirement nvarchar(max),      
           @IsMedicalCardRequired nvarchar(max),      
           @TotalServiceYear numeric(4,0),      
           @TotalServiceMonth numeric(4,0),      
           @TotalServiceDays numeric(4,0),      
           @AdditionalServiceYears numeric(4,0),      
           @AdditionalServiceMonth numeric(4,0),      
           @AdditionalServiceDays numeric(4,0),      
           @ServiceNotCountedYear numeric(4,0),      
           @ServiceNotCountedMonth numeric(4,0),      
           @ServiceNotCountedDays numeric(4,0),      
           @QualifyingServiceYear numeric(4,0),      
           @QualifyingServiceMonth numeric(4,0),      
           @QualifyingServiceDays numeric(4,0),      
           @ServiceDetails nvarchar(max),      
           @Perticulars nvarchar(max),      
           @HalfYear nvarchar(max),   
           @BasicPay numeric(18,0),    
      @DA numeric(18,0),  
           @NPA numeric(18,0),      
           @PaySlip1 varbinary(MAX) = null,      
           @PaySlip2 varbinary(MAX) = null,      
           @PaySlip3 varbinary(MAX) = null,   
     @Designation varchar(max),  
     @LevelPayment int,  
        @EmployeeRegistrationId uniqueidentifier ,      
     @CreatedBy nvarchar(max) = null ,        
  @ModifideBy nvarchar(max) = null,        
  @ReturnMsg nvarchar(max) output       
AS      
BEGIN      
begin try        
if(@Id is null)      
begin      
      
 INSERT INTO [dbo].[ServiceHistory]      
           ([DTLDepartmentName]      
           --,[CategoryOfEmployeement]      
           ,[ReasonOfRetirement]      
           ,[IsMedicalCardRequired]      
           ,[TotalServiceYear]      
           ,[TotalServiceMonth]      
           ,[TotalServiceDays]      
           ,[AdditionalServiceYears]      
           ,[AdditionalServiceMonth]      
           ,[AdditionalServiceDays]      
           ,[ServiceNotCountedYear]      
           ,[ServiceNotCountedMonth]      
           ,[ServiceNotCountedDays]      
           ,[QualifyingServiceYear]      
           ,[QualifyingServiceMonth]      
           ,[QualifyingServiceDays]      
           ,[ServiceDetails]      
           ,[Perticulars]      
           ,[HalfYear]      
           ,[BasicPay]  
     ,[DA]  
           ,[NPA]      
           ,[PaySlip1]      
           ,[PaySlip2]      
           ,[PaySlip3]      
     ,[Designation]  
     ,[LevelPayment]  
     ,[EmployeeRegistrationId]      
     ,[CreatedBy]      
           )      
     VALUES      
           (      
           @DTLDepartmentName,      
           --@CategoryOfEmployeement,      
           @ReasonOfRetirement,      
           @IsMedicalCardRequired,      
           @TotalServiceYear,      
           @TotalServiceMonth,      
           @TotalServiceDays,      
           @AdditionalServiceYears,      
           @AdditionalServiceMonth,      
           @AdditionalServiceDays,      
           @ServiceNotCountedYear,      
           @ServiceNotCountedMonth,      
           @ServiceNotCountedDays,      
           @QualifyingServiceYear,      
           @QualifyingServiceMonth,      
           @QualifyingServiceDays,      
           @ServiceDetails,      
           @Perticulars,      
           @HalfYear,      
           @BasicPay,   
      @DA,  
           @NPA,      
           @PaySlip1,      
           @PaySlip2,      
           @PaySlip3   
     ,@Designation  
     ,@LevelPayment,  
     @EmployeeRegistrationId,      
     @CreatedBy      
     )      
     end      
     else      
     begin      
     update [ServiceHistory] set      
      [DTLDepartmentName] = @DTLDepartmentName      
           --,[CategoryOfEmployeement] = @CategoryOfEmployeement      
           ,[ReasonOfRetirement] = @ReasonOfRetirement      
           ,[IsMedicalCardRequired] = @IsMedicalCardRequired      
           ,[TotalServiceYear] = @TotalServiceYear      
           ,[TotalServiceMonth] = @TotalServiceMonth      
           ,[TotalServiceDays] = @TotalServiceDays      
           ,[AdditionalServiceYears] = @AdditionalServiceYears      
           ,[AdditionalServiceMonth] = @AdditionalServiceMonth      
           ,[AdditionalServiceDays] = @AdditionalServiceDays      
           ,[ServiceNotCountedYear] = @ServiceNotCountedYear      
           ,[ServiceNotCountedMonth] = @ServiceNotCountedMonth      
           ,[ServiceNotCountedDays] = @ServiceNotCountedDays      
           ,[QualifyingServiceYear] = @QualifyingServiceYear      
           ,[QualifyingServiceMonth] = @QualifyingServiceMonth      
           ,[QualifyingServiceDays] = @QualifyingServiceDays      
           ,[ServiceDetails] = @ServiceDetails      
           ,[Perticulars] =@Perticulars      
           ,[HalfYear] = @HalfYear      
           ,[BasicPay] = @BasicPay   
      ,[DA] = @DA   
           ,[NPA] = @NPA      
     ,[Designation] = @Designation  
     ,[LevelPayment] = @LevelPayment  
           ,[PaySlip1] = case when @PaySlip1 is not null then @PaySlip1 else PaySlip1 end      
           ,[PaySlip2] = case when @PaySlip2 is not null then @PaySlip2 else PaySlip2 end   
           ,[PaySlip3] = case when @PaySlip3 is not null then @PaySlip3 else PaySlip3 end   
     ,[ModifideBy] = @ModifideBy      
     ,[ModifideDate] = GETDATE()      
      where ID=@Id and EmployeeRegistrationId = @EmployeeRegistrationId      
     end      
      end try        
 begin catch         
  set @ReturnMsg = ERROR_MESSAGE()         
        
  end catch        
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateEmployeePensionStatus]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_UpdateEmployeePensionStatus]
@EmployeeRegistrationId uniqueidentifier,
@status int
as
begin

update EmployeeRegistration set PPOStatusId = @status where ID = @EmployeeRegistrationId

end
GO
/****** Object:  StoredProcedure [dbo].[UpdateCashlessPhysicalSubmit]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCashlessPhysicalSubmit]
(
		@ClaimId int,
		@PhysicalSubmit bit,	
		@ModifiedBy nvarchar(30) NULL
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[MediclaimCashless] 
		SET PhysicalSubmit = @PhysicalSubmit
			,ModifiedBy = @ModifiedBy
			,ModifiedDate = GETDATE()
		WHERE 
			ClaimId = @ClaimId
      
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
GO
/****** Object:  StoredProcedure [dbo].[UpdateNonCashlessPhysicalSubmit]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateNonCashlessPhysicalSubmit]
(
		@ClaimId int,
		@PhysicalSubmit bit,	
		@ModifiedBy nvarchar(30) NULL
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[MediclaimNonCashless] 
		SET PhysicalSubmit = @PhysicalSubmit
			,ModifiedBy = @ModifiedBy
			,ModifiedDate = GETDATE()
		WHERE 
			ClaimId = @ClaimId
      
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
GO
/****** Object:  StoredProcedure [dbo].[usp_BudgetReport_GetAll]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[usp_BudgetReport_GetAll]       
@FinancialYear varchar(50)  
AS      
BEGIN      

DECLARE @DATA TABLE(
	 AllocationProgram VARCHAR(MAX)
	,AllocatedFunds decimal(18,2)
	,DisbursementPeriod VARCHAR(MAX)
	,DisbursementAuthority varchar(500)
	,DisbursedAmount DECIMAL(18,2)
)

INSERT INTO @DATA
  SELECT 
	AllocationProgram
   ,AllocatedFunds
   ,DisbursementPeriod
   ,DisbursementAuthority
   ,FLOOR(RAND()*(100000-5000+1)+5000) DisbursedAmount 
  FROM BudgetDeclaration 
  WHERE FinancialYear=@FinancialYear    
 
 SELECT AllocationProgram,AllocatedFunds,DisbursementPeriod,DisbursementAuthority,DisbursedAmount, (AllocatedFunds - DisbursedAmount) OutStanding FROM @DATA
 
 END
GO
/****** Object:  StoredProcedure [dbo].[usp_DisbursementReport_GetAll]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

CREATE PROCEDURE [dbo].[usp_DisbursementReport_GetAll]
	
	 @Date Datetime
	,@EmployerName varchar(200)
	,@EmployeeID varchar(10)

AS
BEGIN


DECLARE @StartDateOfMonth Date = (SELECT DATEADD(month, DATEDIFF(month, 0, @Date), 0))
DECLARE @DTLOffice varchar(200) = @EmployerName
DECLARE @ID INT = CAST(@EmployeeID AS int)

--SELECT @StartDateOfMonth
DECLARE @Year varchar(50) =  datepart(yyyy,@StartDateOfMonth)
DECLARE @Month varchar(50) = datename(mm,@StartDateOfMonth)
DECLARE @Mon varchar(50) = datepart(mm,@StartDateOfMonth)

SELECT 
	DISTINCT
	 ER.EmployeeId AS EmployeeID
	,ER.EmployeeName AS EmployeeName
	,ER.DTLOffice AS EmployerName
	,F.Bank AS Bank
	,'' AS Account
	,ISNULL(PS.AdmissiablePension,0) AS Pension
	,ISNULL(TC.MonthlyTDSAmount,0) AS TDSAmount	
	,CAST(CASE WHEN (RA.[ChangeType] = 'Allowance') THEN 0
		ELSE ISNULL(RA.ApplicableAmount,0) END AS decimal(18,2)) AS RecoveryAmount
	,CAST(CASE WHEN (RA.[ChangeType] = 'Allowance') THEN ISNULL(RA.ApplicableAmount,0) 
		ELSE 0 END AS decimal(18,2)) AS AllowanceAmount
	,CAST(CASE WHEN (RA.[ChangeType] = 'Allowance') THEN dbo.CalculateAdmissiblePension(RA.[ChangeType],ISNULL(PS.AdmissiablePension,0),ISNULL(TC.MonthlyTDSAmount,0),0,ISNULL(RA.ApplicableAmount,0),ISNULL(AQP.IncrementedAmount,0))
		ELSE dbo.CalculateAdmissiblePension(RA.[ChangeType],ISNULL(PS.AdmissiablePension,0),ISNULL(TC.MonthlyTDSAmount,0),ISNULL(RA.ApplicableAmount,0),0,ISNULL(AQP.IncrementedAmount,0))  END AS decimal(18,2)) AS AdmissiblePension
	,CAST(ISNULL(AQP.IncrementedAmount,0) AS decimal(18,2)) AS AQPAmount
	,(@Month + '/' + @Year) AS 'Month'
	
FROM dbo.EmployeeRegistration ER
JOIN dbo.PensionSlip PS ON ER.ID = PS.EmployeeRegistrationId
JOIN dbo.Form5 F ON F.EmployeeRegistrationId = ER.ID
LEFT JOIN dbo.TDSCalculation TC ON TC.EmployeeRegistrationId = ER.ID 
LEFT JOIN dbo.AdditionalQuantamPay AQP ON AQP.EmployeeRegistrationId = ER.ID AND AQP.FromDate <= @StartDateOfMonth AND AQP.ToDate >= @StartDateOfMonth
LEFT JOIN dbo.RecoveryAllowance RA ON RA.EmployeeRegistrationId = ER.ID AND RA.FromDate <= @StartDateOfMonth AND RA.ToDate >=@StartDateOfMonth
WHERE (@DTLOffice IS NULL OR ER.DTLOffice = @DTLOffice) AND (@ID IS NULL OR ER.EmployeeId = @ID)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCasetDetailsById]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetCasetDetailsById]
@Id uniqueIdentifier
AS
BEGIN
SET NOCOUNT ON
SELECT * FROM dbo.LegalCases WHERE Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertUpdateAddQtmPaymentDetails]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertUpdateAddQtmPaymentDetails]
(
	 @Id uniqueIdentifier = null 
	,@EmployeeRegistrationId uniqueIdentifier
	,@PensionerName varchar(50)
	,@DOB date
	,@EmployeeID int
	,@EmployerName varchar(500)
	,@CurrentAge int
	,@AgeGroup varchar(50)
	,@MonthlyPension decimal(18,6)
	,@IncrementPercentage varchar(50)
	,@IncrementAmount decimal(18,6)
	,@AQPMonthlyPension decimal(18,6)
	,@FromDate date
	,@ToDate date
	,@CreatedBy nvarchar(max) = null   
	,@ModifideBy nvarchar(max) = null 
	,@ReturnMsg varchar(max) output
)
AS
BEGIN

BEGIN TRANSACTION

BEGIN TRY
IF(@Id IS NULL)
BEGIN
INSERT INTO dbo.AdditionalQuantamPay
(
	 EmployeeRegistrationId
	,PensionerName
	,DOB
	,EmployeeId
	,EmployerName
	,CurrentAge
	,AgeGroup
	,MonthlyPension
	,AQPercentage
	,IncrementedAmount
	,MonthlyAQPension
	,FromDate
	,ToDate
	,CreatedBy
)
VALUES
(
     @EmployeeRegistrationId
	,@PensionerName
	,@DOB
	,@EmployeeID
	,@EmployerName
	,@CurrentAge
	,@AgeGroup
	,@MonthlyPension
	,@IncrementPercentage
	,@IncrementAmount
	,@AQPMonthlyPension
	,@FromDate
	,@ToDate
	,@CreatedBy
)
set @ReturnMsg ='add'

END
ELSE
BEGIN
UPDATE dbo.AdditionalQuantamPay
SET 
	 ID = @Id
	,EmployeeRegistrationId = @EmployeeRegistrationId
	,PensionerName = @PensionerName
	,DOB = @DOB
	,EmployeeId = @EmployeeID
	,EmployerName = @EmployerName
	,CurrentAge = @CurrentAge
	,AgeGroup = @AgeGroup
	,MonthlyPension = @MonthlyPension
	,AQPercentage = @IncrementPercentage
	,IncrementedAmount = @IncrementAmount
	,MonthlyAQPension = @AQPMonthlyPension
	,FromDate = @FromDate
	,ToDate = @ToDate
	,ModifiedBy = @ModifideBy
	,ModifiedDate = GETDATE()
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
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertUpdateRecoveryAllowanceDetails]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_InsertUpdateRecoveryAllowanceDetails]
(
	 @Id uniqueIdentifier = null 
	,@EmployeeRegistrationId uniqueIdentifier
	,@PensionerName varchar(50)
	,@EmployeeID int
	,@EmployerName varchar(500)
	,@ChangeType varchar(500)
	,@Reason varchar(500)
	,@TypeOfRecovery varchar(500)
	,@RecoveryAmount decimal(18,2)
	,@RecoveryOption varchar(100)
	,@MonthlyPension decimal(18,6)
	,@ApplicableAmount decimal(18,6)
	,@MonthlyPensionAfter decimal(18,6)
	,@FromDate date
	,@ToDate date
	,@CreatedBy nvarchar(max) = null   
	,@ModifideBy nvarchar(max) = null 
	,@ReturnMsg varchar(max) output
)
AS
BEGIN

BEGIN TRANSACTION

BEGIN TRY
IF(@Id IS NULL)
BEGIN
INSERT INTO dbo.RecoveryAllowance
(
	 EmployeeRegistrationId
	,PensionerName
	,EmployeeId
	,EmployerName
	,ChangeType
	,Reason
	,TypeOfRecovery
	,RecoveryAmount
	,RecoveryOption
	,MonthlyPension
	,ApplicableAmount
	,MonthlyPensionAfter
	,FromDate
	,ToDate
	,CreatedBy
)
VALUES
(
     @EmployeeRegistrationId
	,@PensionerName
	,@EmployeeID
	,@EmployerName
	,@ChangeType
	,@Reason
	,@TypeOfRecovery
	,@RecoveryAmount
	,@RecoveryOption
	,@MonthlyPension
	,@ApplicableAmount
	,@MonthlyPensionAfter
	,@FromDate
	,@ToDate
	,@CreatedBy
)
set @ReturnMsg ='add'

END
ELSE
BEGIN
UPDATE dbo.RecoveryAllowance
SET 
	 ID = @Id
	,EmployeeRegistrationId = @EmployeeRegistrationId
	,PensionerName = @PensionerName
	,EmployeeId = @EmployeeID
	,EmployerName = @EmployerName
	,ChangeType = @ChangeType
	,Reason = @Reason
	,TypeOfRecovery=@TypeOfRecovery
	,RecoveryAmount=@RecoveryAmount
	,RecoveryOption=@RecoveryOption
	,MonthlyPension = @MonthlyPension
	,ApplicableAmount = @ApplicableAmount
	,MonthlyPensionAfter = @MonthlyPensionAfter
	,FromDate = @FromDate
	,ToDate = @ToDate
	,ModifiedBy = @ModifideBy
	,ModifiedDate = GETDATE()
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
GO
/****** Object:  StoredProcedure [dbo].[usp_LegalCases_GetAll]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_LegalCases_GetAll]

AS

BEGIN

SELECT ID
,CaseNo
,CourtType
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
FROM LegalCases

END
GO
/****** Object:  StoredProcedure [dbo].[usp_LegalCasesDetails_Upsert]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_LegalCasesDetails_Upsert]
(
	 @ID uniqueIdentifier = null 
	,@CaseNo varchar(200)
	,@CourtType varchar(100)
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
)
VALUES
(
     @CaseNo
	,@CourtType
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
)
set @ReturnMsg ='add'

END
ELSE
BEGIN
UPDATE dbo.LegalCases
SET 
	 CaseNo = @CaseNo
	,CourtType = @CourtType
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
GO
/****** Object:  StoredProcedure [dbo].[usp_Pensioner_GetAll]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Pensioner_GetAll]
	
AS
BEGIN

DECLARE @Data Table
(
	[ID] [UNIQUEIDENTIFIER],
	[EmployeeRegistrationId] [UNIQUEIDENTIFIER],
	[PensionerName] [VARCHAR](50),
	[DOB] [DATE],
	[EmployeeId] [BIGINT],
	[EmployerName] [VARCHAR](500),
	[CurrentAge] [INT],
	[AgeGroup] [VARCHAR](50),
	[MonthlyPension] [DECIMAL](16,8),
	[AQPercentage] [VARCHAR](50),
	[IncrementedAmount] [DECIMAL](16,8),
	[MonthlyAQPension] [DECIMAL](16,8),
	[FromDate] [DATE],
	[ToDate] [DATE],
	[CreatedDate] [DATETIME],
	[ModifiedDate] [DATETIME],
	[CreatedBy] [VARCHAR](50),
	[ModifiedBy] [VARCHAR](50)
)

BEGIN
INSERT INTO @Data
SELECT 
	   AQP.[ID]
	  ,AQP.[EmployeeRegistrationId]
      ,AQP.[PensionerName]
      ,AQP.[DOB]
      ,AQP.[EmployeeId]
      ,AQP.[EmployerName]
      ,AQP.[CurrentAge]
      ,AQP.[AgeGroup]
      ,PS.AdmissiablePension
      ,AQP.[AQPercentage]
      ,AQP.[IncrementedAmount]
      ,(PS.AdmissiablePension + AQP.[IncrementedAmount])
      ,AQP.[FromDate]
      ,AQP.[ToDate]
      ,AQP.[CreatedDate]
      ,AQP.[ModifiedDate]
      ,AQP.[CreatedBy]
      ,AQP.[ModifiedBy]
FROM dbo.AdditionalQuantamPay AQP
JOIN dbo.EmployeeRegistration ER ON ER.EmployeeId = AQP.EmployeeId
JOIN dbo.PensionSlip PS ON ER.ID = PS.EmployeeRegistrationId

END

SELECT 
	   AQP.[ID]
	  ,AQP.[EmployeeRegistrationId]
      ,AQP.[PensionerName]
      ,AQP.[DOB]
      ,AQP.[EmployeeId]
      ,AQP.[EmployerName]
      ,AQP.[CurrentAge]
      ,AQP.[AgeGroup]
      ,AQP.[MonthlyPension]
      ,AQP.[AQPercentage] as IncrementPercentage
      ,AQP.[IncrementedAmount] IncrementAmount
      ,AQP.[MonthlyAQPension] AQPMonthlyPension
      ,AQP.[FromDate]
      ,AQP.[ToDate]
      ,AQP.[CreatedDate]
      ,AQP.[ModifiedDate]
      ,AQP.[CreatedBy]
      ,AQP.[ModifiedBy]
FROM @Data AQP

UNION 
	   

SELECT  
		NULL ID
       ,ER.ID EmployeeRegistrationId
	   ,ER.EmployeeName PensionerName
	   ,CAST(ER.DOB AS DATE) DOB
       ,ER.EmployeeId 
	   ,ER.DTLOffice EmployerName
	   ,CAST(DATEDIFF(yy,ER.DOB,GETDATE()) AS INT) CurrentAge
	   ,'' AgeGroup
	   ,PS.AdmissiablePension MonthlyPension
	   ,'' as IncrementPercentage
	   ,0 IncrementAmount
	   ,0 AQPMonthlyPension
	   ,''
	   ,''
	   ,''
	   ,''
	   ,''
	   ,''
FROM dbo.EmployeeRegistration ER 
JOIN dbo.PensionSlip PS ON ER.ID = PS.EmployeeRegistrationId
WHERE NOT EXISTS (SELECT 1 FROM @Data D WHERE D.EmployeeId = ER.EmployeeId)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_Pensioner_RecoveryAllowance_GetAll]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Pensioner_RecoveryAllowance_GetAll]
	
AS
BEGIN

DECLARE @Data Table
(
	[ID] [UNIQUEIDENTIFIER],
	[EmployeeRegistrationId] [UNIQUEIDENTIFIER],
	[PensionerName] [VARCHAR](50),
	[EmployeeId] [BIGINT],
	[EmployerName] [VARCHAR](500),
	[ChangeType] [VARCHAR](500),
	[Reason] [VARCHAR](500),
	[MonthlyPension] [DECIMAL](16,8),
	[ApplicableAmount] [DECIMAL](16,8),
	[MonthlyPensionAfter] [DECIMAL](16,8),
	[FromDate] [DATE],
	[ToDate] [DATE],
	[CreatedDate] [DATETIME],
	[ModifiedDate] [DATETIME],
	[CreatedBy] [VARCHAR](50),
	[ModifiedBy] [VARCHAR](50)
)

BEGIN

INSERT INTO @Data
SELECT 
	   AQP.[ID]
	  ,AQP.[EmployeeRegistrationId]
      ,AQP.[PensionerName]
      ,AQP.[EmployeeId]
      ,AQP.[EmployerName]
      ,AQP.[ChangeType]
      ,AQP.[Reason]
      ,PS.AdmissiablePension
      ,AQP.[ApplicableAmount]
	  ,CASE WHEN (AQP.[ChangeType] = 'Allowance') THEN (PS.AdmissiablePension + AQP.[ApplicableAmount])
		ELSE (PS.AdmissiablePension - AQP.[ApplicableAmount]) END
      ,AQP.[FromDate]
      ,AQP.[ToDate]
      ,AQP.[CreatedDate]
      ,AQP.[ModifiedDate]
      ,AQP.[CreatedBy]
      ,AQP.[ModifiedBy]
FROM dbo.RecoveryAllowance AQP
JOIN dbo.EmployeeRegistration ER ON ER.EmployeeId = AQP.EmployeeId
JOIN dbo.PensionSlip PS ON ER.ID = PS.EmployeeRegistrationId

END

SELECT 
	   AQP.[ID]
	  ,AQP.[EmployeeRegistrationId]
      ,AQP.[PensionerName]
      ,AQP.[EmployeeId]
      ,AQP.[EmployerName]
      ,AQP.[ChangeType]
      ,AQP.[Reason]
      ,AQP.[MonthlyPension]
      ,AQP.[ApplicableAmount]
      ,AQP.[MonthlyPensionAfter]
      ,AQP.[FromDate]
      ,AQP.[ToDate]
      ,AQP.[CreatedDate]
      ,AQP.[ModifiedDate]
      ,AQP.[CreatedBy]
      ,AQP.[ModifiedBy]
FROM @Data AQP

UNION 
	   

SELECT  
		NULL ID
       ,ER.ID EmployeeRegistrationId
	   ,ER.EmployeeName PensionerName
       ,ER.EmployeeId 
	   ,ER.DTLOffice EmployerName
	   ,'' ChangeType
	   ,'' Reason
	   ,PS.AdmissiablePension MonthlyPension
	   ,0 as ApplicableAmount
	   ,0 MonthlyPensionAfter
	   ,''
	   ,''
	   ,''
	   ,''
	   ,''
	   ,''
FROM dbo.EmployeeRegistration ER 
JOIN dbo.PensionSlip PS ON ER.ID = PS.EmployeeRegistrationId
WHERE NOT EXISTS (SELECT 1 FROM @Data D WHERE D.EmployeeId = ER.EmployeeId)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_PensionReport_GetAll]    Script Date: 12/15/2021 9:52:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 

CREATE PROCEDURE [dbo].[usp_PensionReport_GetAll]
	 @Date Datetime 
	,@EmployerName varchar(200)
	,@EmployeeID varchar(10)

AS

BEGIN


DECLARE @StartDateOfMonth Date = (SELECT DATEADD(month, DATEDIFF(month, 0, @Date), 0))
DECLARE @DTLOffice varchar(200) = @EmployerName
DECLARE @ID INT = CAST(@EmployeeID AS int)

--SELECT @StartDateOfMonth
DECLARE @Year varchar(50) =  datepart(yyyy,@StartDateOfMonth)
DECLARE @Month varchar(50) = datename(mm,@StartDateOfMonth)
DECLARE @Mon varchar(50) = datepart(mm,@StartDateOfMonth)

DECLARE @DATA TABLE(
	 EmployeeID VARCHAR(MAX)
	,EmployeeName VARCHAR(MAX)
	,Employer VARCHAR(MAX)
	,Contribution DECIMAL(18,2)
	,[Month] VARCHAR(50)
	,PensionAmount DECIMAL(18,2)
)

INSERT INTO @DATA
SELECT 
	DISTINCT
	 ER.EmployeeId AS EmployeeID
	,ER.EmployeeName AS EmployeeName
	,ER.DTLOffice AS Employer
	--,ISNULL(PS.AdmissiablePension,0) AS PensionAmount
	,FLOOR(RAND()*(10000-5000+1)+5000) AS Contribution
	,(@Month + '/' + @Year) AS 'Month'
	,CAST(CASE WHEN (RA.[ChangeType] = 'Allowance') THEN dbo.CalculateAdmissiblePension(RA.[ChangeType],ISNULL(PS.AdmissiablePension,0),ISNULL(TC.MonthlyTDSAmount,0),0,ISNULL(RA.ApplicableAmount,0),ISNULL(AQP.IncrementedAmount,0))
		ELSE dbo.CalculateAdmissiblePension(RA.[ChangeType],ISNULL(PS.AdmissiablePension,0),ISNULL(TC.MonthlyTDSAmount,0),ISNULL(RA.ApplicableAmount,0),0,ISNULL(AQP.IncrementedAmount,0))  END AS decimal(18,2)) AS PensionAmount
FROM dbo.EmployeeRegistration ER
JOIN dbo.PensionSlip PS ON ER.ID = PS.EmployeeRegistrationId
JOIN dbo.Form5 F ON F.EmployeeRegistrationId = ER.ID
LEFT JOIN dbo.TDSCalculation TC ON TC.EmployeeRegistrationId = ER.ID 
LEFT JOIN dbo.AdditionalQuantamPay AQP ON AQP.EmployeeRegistrationId = ER.ID AND AQP.FromDate <= @StartDateOfMonth AND AQP.ToDate >= @StartDateOfMonth
LEFT JOIN dbo.RecoveryAllowance RA ON RA.EmployeeRegistrationId = ER.ID AND RA.FromDate <= @StartDateOfMonth AND RA.ToDate >=@StartDateOfMonth
WHERE (@DTLOffice IS NULL OR ER.DTLOffice = @DTLOffice) AND (@ID IS NULL OR ER.EmployeeId = @ID)


SELECT EmployeeID,EmployeeName,Employer,Contribution,[Month],PensionAmount FROM @DATA 
UNION
SELECT '','Total','',SUM(Contribution),'',SUM(PensionAmount) FROM @DATA 



END
GO
USE [master]
GO
ALTER DATABASE [DTLPension] SET  READ_WRITE 
GO
