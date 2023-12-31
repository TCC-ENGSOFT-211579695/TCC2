USE [TCC]
GO
/****** Object:  Table [dbo].[ProductionData]    Script Date: 25/06/2023 19:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionData](
	[CoilCode] [varchar](50) NOT NULL,
	[RollingBegin] [datetime] NULL,
	[RollingEnd] [datetime] NULL,
 CONSTRAINT [PK_ProductionData] PRIMARY KEY CLUSTERED 
(
	[CoilCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ProductionData] ([CoilCode], [RollingBegin], [RollingEnd]) VALUES (N'A000000001', NULL, NULL)
INSERT [dbo].[ProductionData] ([CoilCode], [RollingBegin], [RollingEnd]) VALUES (N'A000000002', NULL, NULL)
INSERT [dbo].[ProductionData] ([CoilCode], [RollingBegin], [RollingEnd]) VALUES (N'A000000003', NULL, NULL)
GO
