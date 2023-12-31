USE [TCC]
GO
/****** Object:  Table [dbo].[CoilDataBin]    Script Date: 25/06/2023 19:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoilDataBin](
	[Timestamp] [datetime] NULL,
	[CoilCode] [varchar](50) NULL,
	[Blob] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CoilDataBin]  WITH CHECK ADD  CONSTRAINT [FK_CoilDataBin_ProductionData] FOREIGN KEY([CoilCode])
REFERENCES [dbo].[ProductionData] ([CoilCode])
GO
ALTER TABLE [dbo].[CoilDataBin] CHECK CONSTRAINT [FK_CoilDataBin_ProductionData]
GO
