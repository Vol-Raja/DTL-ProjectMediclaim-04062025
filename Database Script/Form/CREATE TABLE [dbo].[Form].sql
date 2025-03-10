USE [DTLPension]
GO

/****** Object:  Table [dbo].[Form]    Script Date: 17-10-2022 8.37.19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Form](
	[ID] [uniqueidentifier] NOT NULL,
	[TitleInEnglish] [nvarchar](250) NOT NULL,
	[TitleInHindi] [nvarchar](250) NULL,
	
	[AttachmentTitleInEnglish] [nvarchar](max) NULL,
	[AttachmentTitleInHindi] [nvarchar](max) NULL,
	[AttachmentFileInEnglish] [varbinary](max) NULL,
	[EnglishFileName] [nvarchar](250) NULL,
	[EnglishContentType] [nvarchar](250) NULL,
	[AttachmentFileInHindi] [varbinary](max) NULL,
	[HindiFileName] [nvarchar](250) NULL,
	[HindiContentType] [nvarchar](250) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Form] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Form] ADD  CONSTRAINT [DF_Form_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Form] ADD  CONSTRAINT [DF_Form_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


