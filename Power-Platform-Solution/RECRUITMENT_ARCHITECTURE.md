# Recruitment Agency Solution - Architecture & Integration

## Document Overview

This document describes the high-level architecture, user workflows, system interactions, and integration patterns for the recruitment agency Power Platform solution.

---

## Solution Architecture Overview

```
┌────────────────────────────────────────────────────────────────────────┐
│                        RECRUITMENT PLATFORM SOLUTION                   │
└────────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────┐
│                          USER LAYER                                 │
├─────────────────────────────────────────────────────────────────────┤
│  Recruiter      │  Hiring Mgr  │  Client Portal  │  Finance │ Exec │
│  (Canvas App)   │(Model-Driven)│  (Power Pages)  │(Reporting)│(BI) │
└─────────────────────────────────────────────────────────────────────┘
                                ↓
┌─────────────────────────────────────────────────────────────────────┐
│                     ORCHESTRATION LAYER                             │
├─────────────────────────────────────────────────────────────────────┤
│           Power Automate Workflows & Intelligence                   │
│  • Interview Scheduling & Reminders                                │
│  • Feedback Collection & Analysis                                  │
│  • Approval Routing & Status Updates                               │
│  • Candidate & Placement Notifications                             │
│  • Invoice Generation (Future)                                     │
│  • AI Builder Models for Matching & Prediction                     │
└─────────────────────────────────────────────────────────────────────┘
                                ↓
┌─────────────────────────────────────────────────────────────────────┐
│                       DATA LAYER                                    │
├─────────────────────────────────────────────────────────────────────┤
│                       DATAVERSE                                     │
│  ┌─────────────┐ ┌──────────────┐ ┌──────────────┐                 │
│  │ Candidates  │ │  Positions   │ │Applications  │                 │
│  └─────────────┘ └──────────────┘ └──────────────┘                 │
│                                                                     │
│  ┌─────────────┐ ┌──────────────┐ ┌──────────────┐                 │
│  │ Interviews  │ │  Feedback    │ │ Placements   │                 │
│  └─────────────┘ └──────────────┘ └──────────────┘                 │
│                                                                     │
│  ┌──────────────────────────────────────────────────────────────┐   │
│  │              Clients (Metadata)                              │   │
│  └──────────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────┘
                                ↓
┌─────────────────────────────────────────────────────────────────────┐
│                    INTEGRATION LAYER                                │
├─────────────────────────────────────────────────────────────────────┤
│   Outlook    │  SharePoint  │  Accounting Sys  │  LinkedIn/ATS     │
│ (Email/Cal)  │  (Documents) │   (Future)       │  (Future)         │
└─────────────────────────────────────────────────────────────────────┘
```

---

## End-to-End Business Process Flow

### Phase 1: Candidate Sourcing & Profile Creation

**Actors:** Sarah (Recruiter)

**Steps:**

1. **Candidate Intake**
   - Sarah opens Canvas Power App (Recruiter Dashboard)
   - Navigates to "New Candidate"
   - Enters candidate profile: Name, Email, Phone, Location, Skills, Experience
   - Uploads resume to SharePoint (via Power Apps file control)
   - Clicks Save

2. **Data Persistence**
   - Power Apps writes candidate record to Dataverse
   - Resume file is stored in SharePoint and linked to candidate record
   - Candidate.Status set to "Active"
   - Candidate.CreatedDate and CreatedBy populated

3. **Initial Dashboard Update**
   - Canvas App displays confirmation: "Candidate Added Successfully"
   - Candidate appears in "My Candidates" list on recruiter dashboard
   - No notification sent (internal action only)

---

### Phase 2: Job Requisition & Candidate Matching

**Actors:** Jane (Client Hiring Manager), Sarah (Recruiter)

**Steps:**

1. **Client Submits Job Requisition (via Power Pages Portal)**
   - Jane logs into Client Portal with credentials
   - Clicks "New Job Requisition"
   - Fills form:
     - Job Title: "Senior Software Engineer"
     - Description: [detailed role description]
     - Required Skills: Python, JavaScript, React, AWS
     - Experience: 5+ years
     - Location: San Francisco, CA
     - Salary Range: $120-160K
     - Target Fill Date: 30 days from now
   - Submits form

2. **Position Created in Dataverse**
   - Power Pages writes Position record to Dataverse
   - Position.Status = "Open"
   - Position.DatePosted = Today
   - Position.ClientID = ABC Corporation
   - Position.HiringManagerID = Dan (assigned by account manager)
   - Position.AccountManagerID = John (client account manager)

3. **Recruiter Notification (via Power Automate)**
   - Automated workflow triggered: "New Position Submitted"
   - Power Automate sends email to Sarah:
     - Subject: "New Requisition: Senior Software Engineer at ABC Corp"
     - Body: Position details, target fill date, direct link to open in Canvas App
   - Sarah receives email within 2 minutes

4. **Recruiter Reviews & Initiates Candidate Matching**
   - Sarah clicks link in email or navigates to new requisition in Canvas App
   - Views position details
   - Clicks "Find Candidates" button
   - Canvas App queries Dataverse for candidates and calls AI Builder model:
     - AI calculates MatchScore for all "Active" candidates against this position
     - Algorithm: 60% skill match, 30% experience, 10% location
   - Results sorted by match score (highest first)
   - Sarah sees ranked list of candidates (e.g., 87% John Smith, 79% Mary Johnson, 72% Robert Brown)

5. **Candidate Selection & Application Creation**
   - Sarah reviews top 5 candidates
   - For each high-match candidate, clicks "Apply" button
   - Power Apps creates Application record in Dataverse:
     - Application.CandidateID = John Smith
     - Application.PositionID = Senior Software Engineer - ABC Corp
     - Application.MatchScore = 87 (pre-calculated)
     - Application.Status = "Submitted"
     - Application.RecruiterID = Sarah

6. **Candidate Notification (via Power Automate)**
   - Automated workflow: "New Application Opportunity"
   - For each application created, Power Automate:
     - Sends email to candidate: "You've been matched with [Position] at [Company]"
     - Includes position summary and link to apply (if self-service portal exists)
     - Updates Application.DateApplied

---

### Phase 3: Interview Scheduling & Execution

**Actors:** Sarah (Recruiter), Dan (Hiring Manager), John Smith (Candidate)

**Steps:**

1. **Recruiter Advances Candidate to Interview**
   - Sarah reviews all applications for position
   - Decides to move "Submitted" applications to "Screening" stage
   - For top 3 candidates, clicks "Schedule Interview" in Canvas App
   - Form opens:
     - Round: "Phone Screen"
     - Date: [date picker]
     - Time: [time picker]
     - Format: Phone
     - Interviewer: [dropdown showing available hiring managers]
   - Sarah selects Dan as interviewer
   - Clicks "Schedule"

2. **Interview Record Created in Dataverse**
   - Power Apps creates Interview record:
     - InterviewID = INT-004521
     - ApplicationID = APP-003789
     - Round = "Phone Screen"
     - InterviewDate = 2026-05-10
     - InterviewTime = 10:00 AM
     - InterviewerID = Dan
     - Status = "Scheduled"
     - CreatedDate = now

3. **Calendar Invites & Notifications (via Power Automate)**
   - Instant workflow triggered: "Schedule Interview"
   - Power Automate:
     - Creates Outlook calendar event:
       - Title: "Phone Screen - John Smith (Senior Software Engineer)"
       - Time: 2026-05-10 10:00 AM - 10:30 AM
       - Attendees: Dan, Sarah, John Smith
       - Description: Position details, candidate summary, interview guidelines
     - Sends calendar invite to Dan (Outlook): Invite appears in calendar
     - Sends calendar invite to John Smith (Outlook): Candidate receives invite
     - Sends confirmation email to Sarah: Interview scheduled
     - Updates Interview.CalendarEventID with Outlook event ID
   - All three parties receive notifications within 1 minute

4. **Interview Day**
   - Dan receives calendar reminder at 9:00 AM (Outlook)
   - Candidate receives reminder at 9:00 AM (Outlook)
   - At 10:00 AM, Dan calls/video-calls candidate
   - Conducts interview (~30 mins)
   - Takes notes in Interview.Notes field

5. **Interview Completion**
   - Dan marks interview as "Completed"
   - Power Apps updates:
     - Interview.Status = "Completed"
     - Interview.CompletedDate = now

6. **Feedback Collection Notification (via Power Automate)**
   - Automated workflow: "Interview Completed - Request Feedback"
   - Power Automate sends email to Dan:
     - Subject: "Feedback Requested: Phone Screen - John Smith"
     - Body: Link to feedback form with candidate summary
     - Instructions: "Please rate candidate on technical, cultural fit, and provide overall recommendation"

---

### Phase 4: Feedback & Hiring Decision

**Actors:** Dan (Hiring Manager), Sarah (Recruiter)

**Steps:**

1. **Hiring Manager Submits Feedback (Model-Driven App)**
   - Dan clicks link in email or opens Model-Driven App
   - Navigates to Interview record
   - Fills Feedback Form:
     - Technical Score: 4 (out of 5)
     - Cultural Fit Score: 5
     - Comments: "John demonstrated excellent problem-solving skills. Great fit for team culture. Very interested in role."
     - Recommendation: "Strong Yes"
   - Clicks Submit

2. **Feedback Stored & AI Analysis (Dataverse + AI Builder)**
   - Power Apps creates Feedback record:
     - InterviewID = INT-004521
     - InterviewerID = Dan
     - TechnicalScore = 4
     - CulturalFitScore = 5
     - Comments = [as entered]
     - Recommendation = "Strong Yes"
     - DateSubmitted = now
   - AI Builder sentiment analysis runs automatically:
     - Analyzes Comments field
     - Detects positive sentiment (confidence: 0.94)
     - Identifies keywords: "excellent", "great", "interested"
     - Sets SentimentAnalysis = "Positive"
     - Sets SentimentScore = 0.94

3. **Feedback Aggregation & Notification (Power Automate)**
   - If this is the only interviewer: Proceed to step 4
   - If multiple interviewers scheduled: Wait for all feedback
   - Once all feedback received, Power Automate:
     - Aggregates all Feedback records for this interview
     - Calculates AverageScore across all interviewers
     - Sends summary email to Sarah & Dan:
       - "Interview Summary: John Smith - Phone Screen"
       - Technical Score: 4/5
       - Cultural Fit: 5/5
       - Overall Sentiment: Positive
       - Recommendation: Strong Yes (consensus)
     - Updates Application.Status = "Screening Pass"

4. **Advance to Next Round or Make Offer Decision**
   - Sarah reviews feedback summary
   - For this candidate: Decides to move to Technical Round 2
   - Clicks "Schedule Next Interview" in Canvas App
   - Repeats interview scheduling process for Technical Round

---

### Phase 5: Placement Approval & Finalization

**Actors:** Sarah (Recruiter), Dan (Hiring Manager), Jennifer (HR Approver), Jane (Client)

**Steps:**

1. **All Interview Rounds Complete - Ready for Offer**
   - After final interview round, all feedback received
   - Sarah reviews final recommendation: "Recommended for Hire"
   - Application.Status advanced to "Offer Ready"
   - Sarah decides to make offer and initiates placement
   - Clicks "Create Placement" in Canvas App

2. **Placement Record Created & Routed for Approval**
   - Power Apps creates preliminary Placement record:
     - PlacementID = PLACE-006891
     - CandidateID = John Smith
     - PositionID = Senior Software Engineer - ABC Corp
     - Salary = $140,000 (negotiated)
     - Fee = $25,000 (20% of salary)
     - Status = "Pending Approval"
     - ApprovedBy = [empty - waiting]

3. **Conditional Approval Routing (Power Automate)**
   - Instant workflow triggered: "Route Placement for Approval"
   - Logic:
     - If Fee > $20,000 AND Salary > $120,000 → Route to Jennifer (HR Approver)
     - Else → Route to Sarah's manager
   - For this placement: Jennifer receives approval request
   - Power Automate:
     - Sends email to Jennifer:
       - Subject: "Placement Approval Request: John Smith - ABC Corp"
       - Body: Candidate summary, position, salary, fee, interview feedback scores, sentiment analysis
       - CTA: "Approve" or "Reject" button in Model-Driven App form
     - Creates Approval task in Dataverse

4. **Approver Reviews & Decides (Model-Driven App)**
   - Jennifer logs into Model-Driven App
   - Views Placement Approval form with:
     - Candidate profile
     - Position details
     - Placement terms (salary, fee)
     - Interview feedback summary with sentiment analysis
     - AI-predicted placement success likelihood: 87%
   - Jennifer clicks "Approve" with comment: "Approved - strong technical fit, great culture alignment."

5. **Placement Approved & Finalized**
   - Power Apps updates Placement record:
     - Status = "Accepted"
     - ApprovedBy = Jennifer
     - ApprovalDate = now
     - Application.Status = "Placed"
   - Power Automate workflow: "Placement Approved - Finalize"
   - Steps:
     - Generate offer letter template from SharePoint
     - Send offer letter email to John Smith
     - Send acceptance email to Dan (Hiring Manager) and Jane (Client)
     - Set StartDate = agreed date (e.g., 2 weeks from approval)
     - Create 90-day check-in reminder (scheduled for 90 days from start date)
     - Update Position.Status = "Filled"
     - Send notification to Sarah: "Placement Approved - Congratulations!"

6. **Revenue Tracking & Invoice Preparation**
   - Placement.Status = "Offered"
   - Placement.FeeStatus = "Pending" (until candidate starts)
   - Finance team receives notification
   - (Future: Automated invoice generation when status changes to "Started")

---

### Phase 6: 90-Day Check-In & Retention

**Actors:** Dan (Hiring Manager), Sarah (Recruiter)

**Trigger:** 90 days after Placement.StartDate

**Steps:**

1. **Check-In Reminder (Power Automate - Scheduled Flow)**
   - On 90-day anniversary of start date, scheduled flow runs
   - Power Automate sends email to Dan:
     - Subject: "90-Day Check-In: John Smith"
     - Body: Quick questionnaire - "How is the placement going?"
     - Form link in Model-Driven App

2. **Check-In Submission**
   - Dan clicks link and rates placement:
     - NinetyDayCheckInResult = "Thriving"
     - Comments: "John has exceeded expectations. Fully productive."
   - Clicks Submit

3. **Outcome Recording & Success Tracking**
   - Power Apps updates Placement:
     - NinetyDayCheckInCompleted = "Yes"
     - NinetyDayCheckInResult = "Thriving"
     - FinalOutcome = "Successful"
     - Placement.Status = "Completed"
     - Placement.FeeStatus = "Invoiced" (ready for finance)
   - Power BI is updated with successful placement metric
   - Power Automate:
     - Sends notification to Sarah: "Placement successful - John Smith!"
     - Sends notification to Jennifer: "Placement confirmed at 90 days - ready for payment"

---

### Phase 7: Client Portal Visibility (Parallel Throughout)

**Actors:** Jane (Client Hiring Manager)

**Ongoing Access:**

1. **Requisition Dashboard**
   - Jane logs into Power Pages portal
   - Dashboard shows: "Senior Software Engineer - ABC Corp"
   - Status: "Open" → "In Progress" → "Filled"
   - Real-time updates as status changes

2. **Candidate List**
   - Under requisition, Jane sees matched candidates with summary info (skills, experience, no salary data)
   - Each candidate shows stage: "Screening" → "Interviewing" → "Offered" → "Placed"
   - Jane can expand to see candidate summary and feedback score (feedback summary only, not details)

3. **Approval Workflows**
   - If approval needed from client side, Jane receives email in portal
   - Provides feedback/approval in portal form
   - Updates reflected back to internal team

---

## System Interactions & Data Flow

### Real-Time Data Sync

All user interactions in Power Apps immediately write to Dataverse. This allows:
- **Power Pages Portal:** Real-time updates showing candidate progression
- **Power BI Dashboards:** Near real-time metrics (refresh every 30 min)
- **Power Automate Workflows:** Instant triggers on Dataverse record changes

### Event-Driven Architecture

```
User Action (Power Apps)
    ↓
Dataverse Record Created/Updated
    ↓
Power Automate Trigger Activated
    ↓
Multi-Step Workflow Execution
    ├→ Create Calendar Event (Outlook)
    ├→ Send Email Notification (Outlook)
    ├→ Update Related Records (Dataverse)
    ├→ Call AI Builder (for analysis)
    └→ Log Activity
    ↓
Dataverse Updated with Workflow Results
    ↓
Power Apps & Power Pages Reflect Changes
    ↓
Power BI Metrics Refreshed
```

### Batch Operations

For bulk actions (e.g., weekly candidate matching against all open positions):
1. Scheduled Power Automate flow runs daily at 11 PM
2. Queries all open positions
3. For each position:
   - Gets all "Active" candidates with no pending applications
   - Calls AI Builder matching model in batch
   - Creates Application records for high-match candidates (>70%)
   - Sends batch summary email to recruiters

---

## Security & Data Isolation

### Authentication

- **Internal Users** (Recruiters, Hiring Managers, Finance): Entra ID (Azure AD)
- **Client Portal Users** (Client Hiring Managers): Power Pages external authentication (Entra ID or local)
- **Administrators:** Power Platform admin roles in tenant

### Authorization (Role-Based Access Control)

#### Recruiter Role
- Create/Edit/View own and team's candidates
- Create/Edit own applications
- View all positions and interviews
- Create and schedule interviews
- Submit feedback for own interviews
- View all placements
- Cannot create or approve placements

#### Hiring Manager Role
- View own positions and candidates
- Submit interview feedback for assigned interviews
- View own placements
- Cannot create candidates or applications
- Cannot schedule interviews independently

#### HR/Approver Role
- View all placements above fee threshold
- Approve/Reject placement requests
- View all feedback and placement metrics
- Cannot modify other records

#### Client Portal User (External)
- View own job requisitions only
- View matched candidates for own positions only
- View candidate feedback summaries (not details)
- Submit job requisitions
- Cannot access other clients' data
- Cannot access internal recruiter/hiring manager data

#### Finance Role
- View all placements
- View fee and invoice status
- Cannot modify candidate, position, or interview data

#### Admin Role
- Full access to all data
- Manage user roles and permissions
- Manage Dataverse configuration

### Data Isolation

**By Organization:**
- Client 1 can only see their own positions, requisitions, and placements
- Row-level security filters Position, Application, and Placement records

**By Role:**
- Candidates not visible to clients or external users
- Salary and fee fields hidden from candidates and client portal users
- Feedback details hidden from candidates

**By User:**
- Recruiters by default see own candidates; team leads see team candidates
- Hiring managers see only own positions
- Can be customized via business units or team assignments

---

## External System Integrations

### Current Phase Integrations

#### Outlook (Email & Calendar)

**Data Flow:**
- Power Automate creates Outlook calendar events for interviews
- Attendees: Recruiter, Hiring Manager, Candidate
- Event details include meeting link (for video) or location (for in-person)
- Interview reminder emails sent via Outlook

**API Used:** Microsoft Graph API via Power Automate Outlook Connector

**Error Handling:**
- If Outlook calendar creation fails: Log error, retry up to 3 times, email administrator if persistent

#### SharePoint (Document Management)

**Data Flow:**
- Candidate resumes uploaded to SharePoint from Canvas Power App
- Document link stored in Candidate.ResumeFile
- Offer letter templates stored in SharePoint document library
- Finance team accesses SharePoint for placement agreements

**Storage Structure:**
```
/Recruitment
  /Candidates
    /[CandidateID]-[Name]
      - resume.pdf
  /Placements
    /[PlacementID]-[CandidateName]
      - offer_letter.pdf
      - agreement.pdf
```

**Permissions:**
- Only recruiters and admins can upload/access candidate resumes
- Candidates cannot access resume storage

### Future Phase Integrations

#### Accounting System (QuickBooks/Xero)

**Integration Trigger:** When Placement.FeeStatus changes to "Invoiced"

**Data Sent:**
- Invoice details: Client, amount, date, description
- Expected: Invoice ID returned for record-keeping

**Flow:**
```
Placement Created
  ↓
Candidate Starts (Placement.Status = "Started")
  ↓
Power Automate detects status change
  ↓
Calls custom connector to Accounting API
  ↓
Creates invoice in accounting system
  ↓
Returns Invoice ID
  ↓
Updates Placement.InvoiceID & Placement.FeeStatus = "Invoiced"
  ↓
Finance team alerted
```

#### LinkedIn Recruiter / ATS Integration (Future)

**Potential Integration Points:**
- Pull candidate data from LinkedIn into Dataverse (weekly batch)
- Sync job postings to LinkedIn jobs board
- Ingest candidate applications from ATS into Application table

**Data Mapping (Example):**
- LinkedIn candidate → Dataverse Candidate record
- LinkedIn job opening → Dataverse Position record
- ATS application → Dataverse Application record

---

## Error Handling & Resilience

### Power Automate Workflow Retries

- **Failed API Calls:** Automatic retry up to 3 times with exponential backoff
- **Timeout Errors:** Flow requeues with delayed trigger (1 minute)
- **Transient Failures:** Logged but don't block user experience

### Dataverse Capacity Management

- **Threshold Alerts:** Monitor Dataverse storage; alert if >80% capacity
- **Archive Strategy:** Auto-archive closed placements and positions >2 years old
- **Elastic Tables:** Use for high-volume Interview/Feedback data if needed

### Power Apps Error Handling

- **Offline Scenario:** Canvas App displays cached data; syncs when connection restored
- **Validation Errors:** Real-time field validation before submission; user-friendly error messages
- **Concurrency:** Handle simultaneous edits; last-edit-wins or merge approach configurable

---

## Monitoring & Troubleshooting

### Key Metrics to Monitor

- **Power Automate Flow Success Rate:** Target >99%
- **Dataverse Query Performance:** Target <2 sec for standard queries
- **Power Apps Canvas App Load Time:** Target <3 sec
- **Power Pages Portal Uptime:** Target 99.9%
- **Outlook Integration Success:** Target >98%

### Dashboards & Alerts

- **Power Platform Admin Center:** Monitor flow runs, app usage, connector health
- **Application Insights (Future):** Deep analytics on app performance and errors
- **Dataverse Health Check:** Automatic validation of critical tables and relationships

---

## Deployment & Release Strategy

### Phased Rollout

**Phase 1:** Dataverse + Power Apps (Canvas + Model-Driven)
- Target Users: 5 recruiters, 3 hiring managers, 1 admin
- Duration: 4 weeks
- Testing: UAT with sample data

**Phase 2:** Power Pages + Power Automate
- Target Users: Add 2 client portal users, expand internal user base
- Duration: 3 weeks
- Testing: End-to-end workflow testing with real candidates

**Phase 3:** AI Builder Models
- Target Users: All recruiters, hiring managers
- Duration: 2 weeks
- Training: New functionality workshops

**Phase 4:** Power BI Dashboards
- Target Users: Leadership, Finance, Recruiters
- Duration: 2 weeks
- Validation: KPI accuracy against manual counts

**Phase 5:** External Integrations (Accounting, ATS)
- Target Users: Finance, Recruiters
- Duration: Ongoing
- Testing: Sandbox environment first

---

## Change Management & Training

### User Training Plan

| User Role | Training | Duration | Format |
|-----------|----------|----------|--------|
| Recruiter | Canvas App fundamentals, candidate mgmt, matching | 2 hrs | Virtual workshop |
| Hiring Manager | Model-Driven App, feedback forms, approvals | 1 hr | Video + docs |
| Client | Power Pages portal, requisitions, tracking | 1 hr | Email guide |
| Finance | Placement verification, fee tracking, reporting | 1.5 hrs | 1:1 session |
| Admin | System administration, user mgmt, troubleshooting | 4 hrs | Deep dive |

### Support Plan

- **Tier 1 (User Support):** Email support for common questions, response within 4 hours
- **Tier 2 (Admin):** Power Platform admin for configuration changes, response within 24 hours
- **Tier 3 (Development):** Custom development for enhancements, documented in change log

---

## Next Steps

1. **Validate Architecture** with stakeholders and technical team
2. **Create Detailed Design Docs** for each Power Platform component
3. **Set Up Development Environment** in Power Platform tenant
4. **Begin Phase 1 Implementation** - Start with Dataverse table creation
5. **Establish Change Management** process and user training schedule
