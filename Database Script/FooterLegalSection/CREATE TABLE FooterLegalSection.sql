USE [DTLPension]
GO

/****** Object:  Table [dbo].[FooterLegalSection]    Script Date: 29-12-2022 6.34.28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FooterLegalSection](
	[ID] [uniqueidentifier] NOT NULL,
	CopyrightPolicy [nvarchar](max)  NULL,
	 
	CMAPPolicy [nvarchar](max) NULL,
	CAPPolicy [nvarchar](max) NULL,
	ContentReviewPolicy [nvarchar](max) NULL,
	Disclaimer [nvarchar](max) NULL,
	HyperlinkPolicy [nvarchar](max) NULL,
	TermsConditionPolicy [nvarchar](max) NULL,
	ContingencyManagementPlanPolicy [nvarchar](max) NULL,
	PrivacyPolicy [nvarchar](max) NULL,
	WebsiteMonitoringPlanPolicy [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_FooterLegalSection] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[FooterLegalSection] ADD  CONSTRAINT [DF_FooterLegalSection_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[FooterLegalSection] ADD  CONSTRAINT [DF_FooterLegalSection_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


