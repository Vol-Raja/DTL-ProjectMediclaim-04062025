USE [DTLPension]
GO

/****** Object:  Table [dbo].[Testimony]    Script Date: 25-02-2023 7.48.39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Testimony](
	[ID] [uniqueidentifier] NOT NULL,
	[NameEnglish] [nvarchar](250) NULL,
	[NameHindi] [nvarchar](250) NULL,
	[PositionEnglish] [nvarchar](250) NULL,
	[PositionHindi] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[DescriptionHindi] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ModifiedBy] [nvarchar](250) NULL,
	[Image] [varbinary](max) NULL,
	[ImageName] [nvarchar](250) NULL,
	[ImageContentType] [nvarchar](250) NULL,
 
 CONSTRAINT [PK_Testimony] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Testimony] ADD  CONSTRAINT [DF_Testimony_Id]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Testimony] ADD  CONSTRAINT [DF_Testimony_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


