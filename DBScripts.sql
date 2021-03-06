USE [Messaging]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 2/25/2017 9:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupEmails]    Script Date: 2/25/2017 9:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupEmails](
	[GroupId] [int] NOT NULL,
	[EmailId] [int] NOT NULL,
 CONSTRAINT [PK_GroupEmails] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2/25/2017 9:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[MessageText] [nvarchar](4000) NOT NULL,
	[MessageDate] [date] NOT NULL,
	[MessageTime] [time](7) NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MessagingGroups]    Script Date: 2/25/2017 9:57:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessagingGroups](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[FilePath] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MessagingGroups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[GroupEmails]  WITH CHECK ADD  CONSTRAINT [FK_GroupEmails_Emails] FOREIGN KEY([EmailId])
REFERENCES [dbo].[Emails] ([EmailId])
GO
ALTER TABLE [dbo].[GroupEmails] CHECK CONSTRAINT [FK_GroupEmails_Emails]
GO
ALTER TABLE [dbo].[GroupEmails]  WITH CHECK ADD  CONSTRAINT [FK_GroupEmails_MessagingGroups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[MessagingGroups] ([GroupId])
GO
ALTER TABLE [dbo].[GroupEmails] CHECK CONSTRAINT [FK_GroupEmails_MessagingGroups]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_MessagingGroups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[MessagingGroups] ([GroupId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_MessagingGroups]
GO
