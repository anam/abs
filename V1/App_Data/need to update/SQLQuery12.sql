USE [GentleParkHO]
GO
/****** Object:  StoredProcedure [dbo].[GP_GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationID]    Script Date: 03/18/2014 01:21:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [GP_GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationID] 0,53,0,0,' in (3,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,330,331)', '2013-01-01','2013-04-07'
create PROCEDURE [dbo].[GP_GetAllACC_JournalDetailForTrialBalanceLabelWiseWithWorkStationIDShowRoom]
(
@L1 as nvarchar(max),
@L2 as int,
@L3 as int,
@L4 as int,
@WorkStationID as nvarchar(max),
@FromDate as nvarchar(256),
@ToDate as nvarchar(256)
)
AS

declare @SQL as nvarchar(max)
set @SQL ='
    SELECT ACC_JournalDetail.ACC_ChartOfAccountLabel3ID,
     ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text,
     ACC_ChartOfAccountLabel3.Code as ChartOfAccountLabel3Code,
     --ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as WorkStationName,
     --ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID as WorkStation,
    SUM( ACC_JournalDetail.Debit) as Debit,Sum(ACC_JournalDetail.Credit) as Credit
    FROM ACC_JournalDetail
    inner join ACC_ChartOfAccountLabel3 on ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID = ACC_JournalDetail.ACC_ChartOfAccountLabel3ID
    inner join ACC_ChartOfAccountLabel2 on ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID = ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel2ID
    inner join ACC_ChartOfAccountLabel1 on ACC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID = ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel1ID
    left outer join ACC_ChartOfAccountLabel4  on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID = ACC_JournalDetail.WorkStation
    inner join ACC_JournalMaster on ACC_JournalMaster.ACC_JournalMasterID=ACC_JournalDetail.JournalMasterID
  Where ACC_JournalMaster.RowStatusID=1 '
  if @L1<>''
     set @SQL +=' and ACC_ChartOfAccountLabel1.ACC_ChartOfAccountLabel1ID in ('+@L1+')';
  if @L2<>0
     set @SQL +=' and ACC_ChartOfAccountLabel2.ACC_ChartOfAccountLabel2ID = '+cast(@L2 as nvarchar(256))
  if @L3<>0
     set @SQL +=' and ACC_ChartOfAccountLabel3.ACC_ChartOfAccountLabel3ID = '+cast(@L3 as nvarchar(256))
  if @L4<>0
     set @SQL +=' and ACC_JournalDetail.ACC_ChartOfAccountLabel4ID = '+cast(@L4 as nvarchar(256))
 
  set @SQL +=' and ACC_JournalDetail.WorkStation ' +@WorkStationID
  
  if @FromDate<>''
     set @SQL +=' and ACC_JournalMaster.JournalDate >= '''+@FromDate+''''
       
     
     if @ToDate<>''
     set @SQL +=' and ACC_JournalMaster.JournalDate <= '''+@ToDate+''''
		
		--set @SQL +=' order by  ACC_JournalMaster.JournalDate'
		
set @SQL +=' group by ACC_JournalDetail.ACC_ChartOfAccountLabel3ID, 
  ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text,
       ACC_ChartOfAccountLabel3.Code
       --, 
     --ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,
     --ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
      order by ACC_ChartOfAccountLabel3.ChartOfAccountLabel3Text'
     
     
    exec(@SQL); 
RETURN
--select (SUM(Debit)-SUM(Credit)) as Diff,ACC_JournalDetail.JournalMasterID
--into #table
-- from ACC_JournalDetail
-- inner join 
--ACC_JournalMaster on ACC_JournalDetail.JournalMasterID=ACC_JournalMaster.ACC_JournalMasterID
----where
---- (ACC_JournalMaster.JournalDate between '2013-01-01' and '2013-01-31')
----and 

--group by ACC_JournalDetail.JournalMasterID

--select * from #table 
--where Diff<>0
