# Recruitment Agency Solution - Data Model

## Document Overview

This document specifies the Dataverse tables, columns, relationships, and business rules that form the data foundation for the recruitment agency solution.

---

## Dataverse Table Architecture

The data model is organized into five primary domains:

1. **Candidate Management** - Candidate profile and history
2. **Position Management** - Job requisitions and openings
3. **Application & Interview Management** - Candidate-to-position progression
4. **Feedback & Evaluation** - Interview feedback and scoring
5. **Placement & Revenue** - Placement records and financial tracking

---

## Core Tables & Column Specifications

### 1. CANDIDATES Table

**Purpose:** Central repository for all candidate profiles and history

| Column | Type | Required | Description | Example |
|--------|------|----------|-------------|---------|
| CandidateID | Text (Primary Key) | Yes | Unique candidate identifier | CAND-001234 |
| FirstName | Text | Yes | Candidate first name | John |
| LastName | Text | Yes | Candidate last name | Smith |
| Email | Email | Yes | Primary contact email | john.smith@email.com |
| Phone | Phone | Yes | Primary phone number | +1-555-0123 |
| Location | Text | Yes | City and state or country | San Francisco, CA |
| Summary | Memo | No | Professional summary/bio | 10+ years software engineering experience |
| SkillsList | Text | Yes | Comma-separated skills | Python, JavaScript, React, SQL |
| ExperienceYears | Number | Yes | Years of professional experience | 8 |
| DesiredRole | Text | No | Target job title | Senior Software Engineer |
| Certifications | Text | No | Comma-separated certifications | AWS Solutions Architect, Scrum Master |
| Availability | Choice | Yes | Current availability status | **Options:** Immediately, 2 weeks, 1 month, Not Available |
| Status | Choice | Yes | Candidate pipeline status | **Options:** Active, Screening, Interviewing, OnHold, Placed, Inactive |
| SourceChannel | Choice | Yes | How candidate was sourced | **Options:** LinkedIn, Referral, Direct Apply, Agency, Other |
| ResumeFile | File | No | Uploaded resume document | resume_john_smith.pdf |
| LinkedInURL | Text | No | LinkedIn profile URL | https://linkedin.com/in/johnsmith |
| CreatedDate | Date/Time | Yes | Record creation timestamp | 2026-05-01 09:30:00 |
| CreatedBy | Lookup (User) | Yes | User who created record | Sarah (Recruiter) |
| LastUpdated | Date/Time | Yes | Last modification timestamp | 2026-05-05 14:22:00 |
| UpdatedBy | Lookup (User) | Yes | User who last updated record | Sarah (Recruiter) |
| Notes | Memo | No | Internal notes about candidate | Great technical fit, excellent communication |

**Indexes:** Email, Status, SourceChannel, ExperienceYears
**Security Role:** All recruiters can view; only own recruiter or admin can edit

---

### 2. POSITIONS Table

**Purpose:** Track job requisitions, requirements, and openings

| Column | Type | Required | Description | Example |
|--------|------|----------|-------------|---------|
| PositionID | Text (Primary Key) | Yes | Unique position identifier | POS-002456 |
| JobTitle | Text | Yes | Job title/position name | Senior Software Engineer |
| ClientID | Lookup (Clients) | Yes | Client company requesting role | ABC Corporation |
| Description | Memo | Yes | Full job description | Seeking experienced engineer for cloud platform team |
| RequiredSkills | Text | Yes | Comma-separated required skills | Python, JavaScript, React, AWS |
| PreferredSkills | Text | No | Comma-separated preferred skills | Docker, Kubernetes, Machine Learning |
| ExperienceMin | Number | Yes | Minimum years of experience | 5 |
| SalaryRange | Text | No | Salary or range | $120,000 - $160,000 |
| Location | Text | Yes | Job location | San Francisco, CA |
| IsRemote | Choice | Yes | Remote work options | **Options:** On-site, Hybrid, Full Remote |
| Status | Choice | Yes | Requisition status | **Options:** Open, In Progress, Filled, Closed, On Hold |
| HiringManagerID | Lookup (Users) | Yes | Primary hiring manager | Dan (Hiring Manager) |
| AccountManagerID | Lookup (Users) | Yes | Client account manager | John (Account Manager) |
| DatePosted | Date | Yes | When position was opened | 2026-04-15 |
| TargetFillDate | Date | Yes | Desired fill date | 2026-05-30 |
| BudgetAllocation | Currency | No | Budget available for placement | $20,000 |
| Priority | Choice | No | Requisition priority | **Options:** Low, Medium, High, Urgent |
| HoursPerWeek | Number | No | Weekly hours expected | 40 |
| ContractType | Choice | No | Employment type | **Options:** Permanent, Contract, Temporary |
| CreatedDate | Date/Time | Yes | Record creation timestamp | 2026-04-15 10:00:00 |
| CreatedBy | Lookup (User) | Yes | User who created record | Client |
| ClosedDate | Date | No | Date position was filled/closed | 2026-05-20 |

**Indexes:** Status, ClientID, DatePosted, TargetFillDate
**Security Role:** Recruiters see all; hiring managers see own positions; clients see own requisitions only

---

### 3. APPLICATIONS Table

**Purpose:** Track candidate applications to specific positions and job matching

| Column | Type | Required | Description | Example |
|--------|------|----------|-------------|---------|
| ApplicationID | Text (Primary Key) | Yes | Unique application identifier | APP-003789 |
| CandidateID | Lookup (Candidates) | Yes | Applied candidate | John Smith |
| PositionID | Lookup (Positions) | Yes | Target position | Senior Software Engineer - ABC Corp |
| MatchScore | Number | Yes (Auto) | AI-calculated match percentage | 87 |
| MatchReasoning | Memo | No | Why candidate was matched | 8 years exp (meets 5yr min), has all required skills |
| Status | Choice | Yes | Application progression status | **Options:** Submitted, Screening, Screening Pass, Rejected, Interview Scheduled, Interviewing, Offered, Accepted, Declined, Withdrawn |
| DateApplied | Date/Time | Yes | When application was created | 2026-05-01 09:00:00 |
| RecruiterID | Lookup (Users) | Yes | Assigned recruiter | Sarah |
| ScreeningNotesInternal | Memo | No | Recruiter's internal screening notes | Qualifies for next round |
| CandidateRejectionReason | Choice | No | If rejected, why | **Options:** Overqualified, Underqualified, Skill Gap, Compensation Mismatch, Location Mismatch, No Response, Other |
| RejectedDate | Date | No | When candidate was rejected | 2026-05-05 |
| ScreeningCompletedDate | Date | No | Initial screening completion date | 2026-05-02 |
| CreatedDate | Date/Time | Yes | Record creation timestamp | 2026-05-01 09:00:00 |

**Indexes:** Status, CandidateID, PositionID, RecruiterID, MatchScore
**Business Rule:** When status changes to "Rejected", trigger notification to candidate and update Candidate Status if no other active applications

---

### 4. INTERVIEWS Table

**Purpose:** Schedule, track, and record interview events

| Column | Type | Required | Description | Example |
|--------|------|----------|-------------|---------|
| InterviewID | Text (Primary Key) | Yes | Unique interview identifier | INT-004521 |
| ApplicationID | Lookup (Applications) | Yes | Associated application | APP-003789 |
| Round | Choice | Yes | Interview stage/round | **Options:** Phone Screen, Technical Round 1, Technical Round 2, Behavioral, Hiring Manager Round, Executive Round |
| InterviewDate | Date | Yes | Scheduled interview date | 2026-05-10 |
| InterviewTime | Time | Yes | Scheduled interview time | 10:00 AM |
| InterviewerID | Lookup (Users) | Yes | Primary interviewer | Dan (Hiring Manager) |
| SecondaryInterviewerIDs | Lookup (Users) | No | Additional interviewers | [Sarah, Engineering Lead] |
| Status | Choice | Yes | Interview state | **Options:** Scheduled, Completed, No-Show, Rescheduled, Cancelled |
| InterviewFormat | Choice | Yes | How interview conducted | **Options:** Phone, Video, In-Person |
| MeetingLink | Text | No | Video conference URL | https://teams.microsoft.com/... |
| Location | Text | No | Physical location (if in-person) | Conference Room B, 3rd Floor |
| Notes | Memo | No | Pre-interview notes | Confirm availability for 1 hour |
| CompletedDate | Date/Time | No | Actual completion timestamp | 2026-05-10 10:45:00 |
| CalendarEventID | Text | No | Outlook calendar event ID | AAMkADblNTRo... |
| ReminderSentDate | Date/Time | No | When reminder email was sent | 2026-05-09 09:00:00 |
| CandidateNoShowReminderSent | Choice | No | Was no-show reminder sent | **Options:** Yes, No |
| CreatedDate | Date/Time | Yes | Record creation timestamp | 2026-05-05 11:00:00 |

**Indexes:** InterviewDate, Status, InterviewerID, ApplicationID
**Trigger:** When status changes to "Completed", notify feedback collection workflow

---

### 5. FEEDBACK Table

**Purpose:** Collect and aggregate interview feedback and scoring

| Column | Type | Required | Description | Example |
|--------|------|----------|-------------|---------|
| FeedbackID | Text (Primary Key) | Yes | Unique feedback identifier | FB-005643 |
| InterviewID | Lookup (Interviews) | Yes | Associated interview | INT-004521 |
| InterviewerID | Lookup (Users) | Yes | Interviewer providing feedback | Dan (Hiring Manager) |
| TechnicalScore | Choice | Yes (if applicable) | Technical competency rating | **Options:** 1, 2, 3, 4, 5 (1=Poor, 5=Excellent) |
| CulturalFitScore | Choice | Yes | Cultural/soft skills rating | **Options:** 1, 2, 3, 4, 5 |
| CommunicationScore | Choice | No | Communication clarity rating | **Options:** 1, 2, 3, 4, 5 |
| Comments | Memo | No | Detailed feedback and observations | Candidate demonstrated strong problem-solving skills and clear communication. |
| SentimentAnalysis | Choice | (Auto) | AI-detected sentiment from comments | **Options:** Positive, Neutral, Negative |
| SentimentScore | Number | (Auto) | Confidence score for sentiment | 0.87 |
| Recommendation | Choice | Yes | Interviewer recommendation | **Options:** Strong Yes, Yes, Neutral, No, Strong No |
| WouldHireAgain | Choice | No | Would re-interview candidate for future roles | **Options:** Yes, No, Maybe |
| AverageScore | Number | (Auto) | Average of all score fields | 4.2 |
| DateSubmitted | Date/Time | Yes | When feedback was submitted | 2026-05-10 11:30:00 |
| IsComplete | Choice | (Auto) | All required fields populated | **Options:** Yes, No |

**Indexes:** InterviewID, InterviewerID, DateSubmitted, Recommendation
**Business Rule:** Once all feedback for an interview is submitted, notify hiring manager summary
**AI Integration:** AI Builder analyzes Comments field for sentiment and keyword extraction

---

### 6. PLACEMENTS Table

**Purpose:** Record successful placements and track placement lifecycle

| Column | Type | Required | Description | Example |
|--------|------|----------|-------------|---------|
| PlacementID | Text (Primary Key) | Yes | Unique placement identifier | PLACE-006891 |
| CandidateID | Lookup (Candidates) | Yes | Placed candidate | John Smith |
| PositionID | Lookup (Positions) | Yes | Target position filled | Senior Software Engineer - ABC Corp |
| PlacementDate | Date | Yes | When placement approved | 2026-05-15 |
| StartDate | Date | Yes | Candidate's first day | 2026-05-20 |
| OfferAcceptedDate | Date | Yes | When candidate accepted | 2026-05-14 |
| OfferLetter | File | No | Signed offer letter document | offer_john_smith_abc.pdf |
| Status | Choice | Yes | Placement lifecycle status | **Options:** Offered, Accepted, Started, Active, Completed, Terminated |
| Salary | Currency | No | Agreed salary | $140,000 |
| Fee | Currency | Yes | Placement fee charged to client | $25,000 |
| FeePercentage | Number | No | Fee as % of salary (for reference) | 17.86 |
| FeeStatus | Choice | Yes | Invoice and payment state | **Options:** Pending, Invoiced, Paid, Partially Paid, Disputed |
| InvoiceID | Text | No | Reference to generated invoice | INV-008234 |
| InvoiceDate | Date | No | When invoice was sent | 2026-05-20 |
| PaymentDueDate | Date | No | Invoice due date | 2026-06-20 |
| PaymentReceivedDate | Date | No | When payment was received | 2026-06-15 |
| ApprovedBy | Lookup (Users) | Yes | User who approved placement | Jennifer (HR Approver) |
| ApprovalDate | Date/Time | Yes | When approval was granted | 2026-05-14 15:30:00 |
| ApprovalNotes | Memo | No | Approval comments | Approved - all feedback positive, client confirmed start date |
| NinetyDayCheckInDate | Date | No | Scheduled 90-day review date | 2026-08-20 |
| NinetyDayCheckInCompleted | Choice | No | Was check-in completed | **Options:** Yes, No, Not Yet Due |
| NinetyDayCheckInResult | Choice | No | Check-in outcome | **Options:** Thriving, On Track, Concerning, Terminated |
| FinalOutcome | Choice | No | Placement final status | **Options:** Successful, Failed (Candidate), Failed (Employer), Mutual Separation |
| EndDate | Date | No | Last day if placement ended | 2026-08-15 |
| ReasonForTermination | Choice | No | Why placement ended | **Options:** Performance, Fit, Restructuring, Candidate Request, Employer Request, Other |
| CreatedDate | Date/Time | Yes | Record creation timestamp | 2026-05-15 08:00:00 |

**Indexes:** Status, CandidateID, PositionID, FeeStatus, PlacementDate
**Workflow Trigger:** When status changes to "Started", schedule 90-day check-in reminder
**Finance Integration:** When FeeStatus changes to "Invoiced", trigger invoice generation to accounting system

---

### 7. CLIENTS Table

**Purpose:** Track recruitment agency client companies and key contacts

| Column | Type | Required | Description | Example |
|--------|------|----------|-------------|---------|
| ClientID | Text (Primary Key) | Yes | Unique client identifier | CLI-007124 |
| CompanyName | Text | Yes | Client company name | ABC Corporation |
| Industry | Choice | Yes | Client industry | **Options:** Technology, Finance, Healthcare, Manufacturing, Retail, Consulting, Other |
| Website | Text | No | Company website URL | https://www.abccorp.com |
| Address | Text | No | Business address | 123 Market St, San Francisco, CA 94102 |
| PrimaryContactName | Text | Yes | Main contact person | Jane Doe |
| PrimaryContactEmail | Email | Yes | Main contact email | jane@abccorp.com |
| PrimaryContactPhone | Phone | Yes | Main contact phone | +1-555-0456 |
| AccountManagerID | Lookup (Users) | Yes | Assigned account manager | John (Account Manager) |
| PortalUserID | Lookup (Users) | No | Client portal login user | jane@abccorp.com |
| ContractStartDate | Date | Yes | When client relationship began | 2025-01-01 |
| ContractTermMonths | Number | Yes | Contract duration in months | 12 |
| FeePercentage | Number | Yes | Commission % per placement | 20 |
| Status | Choice | Yes | Client relationship status | **Options:** Active, Inactive, Suspended, Prospect |
| TotalPlacementsAllTime | Number | (Auto) | Sum of all placements for client | 15 |
| ActiveOpenPositions | Number | (Auto) | Count of open job requisitions | 3 |
| TotalRevenueAllTime | Currency | (Auto) | Sum of all fees from placements | $375,000 |
| LastActivityDate | Date | (Auto) | Most recent interaction | 2026-05-05 |
| Notes | Memo | No | Internal notes about client | Preferred vendor for tech roles, high placement success rate |
| CreatedDate | Date/Time | Yes | Record creation timestamp | 2025-01-01 09:00:00 |

**Indexes:** Status, AccountManagerID, ContractStartDate
**Security Role:** Account managers see assigned clients; recruiters see all; clients see only own records

---

## Relationships (Table Joins)

| Relationship | Type | Purpose |
|--------------|------|---------|
| Candidates 1-to-Many Applications | 1:N | Track all job applications for a candidate |
| Positions 1-to-Many Applications | 1:N | Track all candidate applications for a position |
| Applications 1-to-Many Interviews | 1:N | Multiple interview rounds per application |
| Interviews 1-to-Many Feedback | 1:N | Multiple feedback entries per interview (one per interviewer) |
| Applications 1-to-1 Placements | 1:1 | Successful application leads to placement |
| Positions 1-to-Many Placements | 1:N | Position may be filled multiple times over time |
| Clients 1-to-Many Positions | 1:N | Client can have multiple job requisitions |

---

## Business Rules & Automation

### Candidate Status Progression

```
Active 
  → Screening (when added to application)
  → Interviewing (when interview scheduled)
  → Placed (when placement approved)
  → Inactive (if no placements after 6 months)
```

### Application Status Workflow

```
Submitted 
  → Screening (recruiter reviews)
  → Screening Pass / Rejected
  → Interview Scheduled
  → Interviewing (collecting feedback)
  → Offered / Rejected
  → Accepted / Declined
  → Placement Created
```

### Placement Fee Recognition

```
Fee Status Flow:
Pending 
  → Invoiced (when placement starts)
  → Paid (when payment received)
```

---

## Data Security & Row-Level Security

### Role-Based Access

| Role | Candidates | Positions | Applications | Interviews | Feedback | Placements |
|------|-----------|-----------|--------------|-----------|----------|-----------|
| **Recruiter** | All | All | Own & Team | Own & Team | View All | View All |
| **Hiring Manager** | View | Own | Own | Own | Submit | View Own |
| **Client Portal User** | None | Own Only | Own Only | Own Only | View Own | View Own |
| **Finance** | View | None | None | None | None | All |
| **Admin** | All | All | All | All | All | All |

### Column-Level Security

- **Salary and Fee columns** are hidden from Candidates and Client Portal users
- **Feedback and Interview scores** are hidden from Candidates

---

## Data Volume & Performance Considerations

### Estimated Data Scale (Year 1)

- **Candidates:** 2,000 records
- **Positions:** 100 active, 500 total
- **Applications:** 5,000 records
- **Interviews:** 3,000 records
- **Feedback:** 4,000 records
- **Placements:** 200 records
- **Clients:** 30 records

### Indexing Strategy

- Primary key indexes on all ID fields
- Secondary indexes on frequently filtered columns (Status, CreatedDate, ClientID, CandidateID, PositionID)
- Consider elastic tables for high-volume interview/feedback data if needed

### Archive Strategy

- Archive completed placements after 2 years
- Maintain feedback and interview records for 3 years for compliance
- Archive closed positions after 1 year

---

## Integration Points

### External System Connections

| System | Data Sync | Frequency | Purpose |
|--------|-----------|-----------|---------|
| Outlook | Bidirectional | Real-time | Interview calendar events, reminders |
| SharePoint | Upload/Download | On-demand | Resume storage, offer letters |
| Accounting System | Outbound | On placement | Invoice data for accounting |
| Candidate Portal (Future) | Inbound | Real-time | Candidate job applications |
| LinkedIn Recruiter (Future) | Import | Weekly | Bulk candidate data import |

---

## Next Steps

1. **Create Dataverse Tables:** Implement all table definitions and relationships
2. **Configure Security Roles:** Set up RBAC and column-level security
3. **Add Business Rules:** Implement automation and validation rules
4. **Test Data Migration:** Prepare scripts to migrate existing candidate/position data
5. **Build Power Apps:** Begin Canvas and Model-Driven app development against finalized schema
