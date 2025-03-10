USE [DTLPension]
GO

/****** Object:  Table [dbo].[Trustee]    Script Date: 08-10-2022 11.09.35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Trustee](
	[ID] [uniqueidentifier] NOT NULL,
	[NameEnglish] [nvarchar](250) NULL,
	[NameHindi] [nvarchar](250) NULL,
	[PositionEnglish] [nvarchar](250) NULL,
	[PositionHindi] [nvarchar](250) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[ModifiedBy] [nvarchar](250) NULL,
	[Image] [varbinary](max) NULL,
	[ImageName] [nvarchar](250) NULL,
	[ImageContentType] [nvarchar](250) NULL,
	[phone] [nvarchar](25) NULL,
 CONSTRAINT [PK_Trustee] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Trustee] ADD  CONSTRAINT [DF_Trustee_Id]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Trustee] ADD  CONSTRAINT [DF_Trustee_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


