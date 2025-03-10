USE [DTLPension]
GO

/****** Object:  Table [dbo].[SocialMediaAccount]    Script Date: 17-10-2022 9.14.51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SocialMediaAccount](
	[ID] [uniqueidentifier] NOT NULL,
	[Facebook] [nvarchar](500)  NULL,
	[Instagram] [nvarchar](500) NULL,
	[Youtube] [nvarchar](500) NULL,
	[Twitter] [nvarchar](500) NULL,
	
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_SocialMediaAccount] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[SocialMediaAccount] ADD  CONSTRAINT [DF_SocialMediaAccount_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[SocialMediaAccount] ADD  CONSTRAINT [DF_SocialMediaAccount_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


