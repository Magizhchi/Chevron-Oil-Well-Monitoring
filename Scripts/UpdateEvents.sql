USE [DataSimulator]
GO

/****** Object:  StoredProcedure [dbo].[UpdateEvents]    Script Date: 4/24/2017 9:32:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[UpdateEvents]

@CompareValue as int,
@ruleid as int,
@tag as varchar(50)

AS
BEGIN
	WITH 
	event_begin as (
		select top(390)
		nextSample.*,
		@ruleid as ruleid
		from history history
		join history nextSample on history.tag = nextSample.tag
		and nextsample.time = (select min(time) from history  h where h.time > history.Time)
		where nextSample.value >= @CompareValue and history.Value < @CompareValue
	),
	event_end as(
		select top(390)
		nextSample.*,
		@ruleid as ruleid
		from history currentSample
		join history nextSample on currentSample.tag = nextsample.tag
		and nextSample.time = (select min(time) from history h where h.time > currentSample.Time)
		where nextSample.value < @CompareValue and currentSample.Value >= @CompareValue
	),
	EventData as(
		select distinct
		event_begin.tag,
		event_begin.ruleid,
		event_begin.time as StartTime,
		isnull(event_end.time,getdate()) as EndTime,
		(select max(value) from history h where event_begin.tag = h.tag and h.time between event_begin.time and isnull(event_end.time,getdate())) MaxValue
		from event_begin 
		left outer join event_end 
		on event_begin.ruleid = event_end.ruleid
		and event_end.time = (select min(time) from event_end where time > event_begin.time)
		where event_begin.tag = @tag
	)

	--Generate history for the designated time and merge into history table
	MERGE into W_Events
	USING EventData
	on W_Events.tag = EventData.tag and W_Events.ruleid = EventData.ruleid and W_Events.StartTime = EventData.StartTime
	when matched then update
		Set W_Events.EndTime = EventData.EndTime, W_Events.MaxValue = EventData.MaxValue
	when not matched then insert (tag, ruleid, MaxValue, StartTime, EndTime)
		values (EventData.tag,EventData.ruleid, EventData.MaxValue, EventData.StartTime,EventData.EndTime);

END



GO

