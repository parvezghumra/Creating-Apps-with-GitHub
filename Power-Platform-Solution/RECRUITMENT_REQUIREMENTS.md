# Recruitment Agency Solution - Requirements & Discovery

## Document Overview

This document captures the business requirements, pain points, current process (As-Is), and envisioned future state (To-Be) for the recruitment agency Power Platform solution.

---

## Current State Analysis (As-Is)

### Pain Points in Current Operations

1. **Candidate Data Fragmentation**
   - Candidate information scattered across multiple spreadsheets, email chains, and disconnected systems
   - Manual data entry prone to errors and duplication
   - Difficult to maintain candidate skill profiles and availability status
   - No centralized view of candidate history across multiple positions

2. **Inefficient Candidate Sourcing & Matching**
   - Recruiters manually review job requirements and candidate profiles
   - Job matching decisions based on gut feel rather than data-driven scoring
   - Time-consuming process to identify top candidates for each position
   - Difficulty tracking why a candidate wasn't suitable for a position

3. **Interview Scheduling Friction**
   - Back-and-forth emails to schedule interviews with candidates, recruiters, and hiring managers
   - No integration with calendars; manual calendar management
   - Candidates miss interview times due to communication gaps
   - No centralized record of who interviewed whom and when

4. **Scattered Feedback & Evaluation**
   - Feedback collected via email, phone notes, or ad-hoc discussions
   - No standardized evaluation criteria across interviews
   - Subjective assessment with no sentiment tracking or consistency
   - Difficult to compare candidate quality across multiple interviews
   - Feedback gets lost or is difficult to retrieve

5. **Limited Pipeline Visibility**
   - Recruiters don't have clear stage progression tracking
   - Leadership cannot see which candidates are close to placement
   - Clients don't know status of their open positions
   - No early warning system for aging open positions

6. **Approval & Placement Delays**
   - Multiple stakeholders (approvers) need to sign off on placements
   - No workflow to route approvals; managed via email
   - Delays in placement finalization due to missing approvals
   - No audit trail of who approved what and when

7. **Invoice & Revenue Tracking Challenges**
   - Manual invoice generation after placement
   - Difficulty tracking which placements have been invoiced
   - Fee calculation errors due to manual tracking
   - No real-time view of outstanding payments

8. **Limited Analytics & Reporting**
   - KPI reporting is manual and ad-hoc
   - Leadership decisions based on incomplete or outdated data
   - Difficult to identify top-performing recruiters
   - No predictive insights on placement success

---

## Future State Vision (To-Be)

### Envisioned Capabilities

1. **Centralized Candidate Database**
   - Single source of truth for all candidate profiles
   - Rich profile data: skills, experience, certifications, preferences, availability
   - Automatic deduplication to prevent duplicate candidate records
   - Activity history tracking (applications, interviews, placements)
   - Integration ready for future LinkedIn Recruiter or ATS system imports

2. **Intelligent Candidate-to-Job Matching**
   - AI algorithm automatically scores candidate-job compatibility
   - Skill matching based on required vs. candidate's actual skills
   - Experience level assessment
   - Geographic and availability alignment
   - Ranked candidate lists so recruiters focus on best fits first

3. **Automated Interview Orchestration**
   - One-click interview scheduling
   - Automatic calendar invitations via Outlook
   - Email reminders to candidates, recruiters, and hiring managers
   - Centralized interview record with date, time, attendees, and outcomes
   - No-show tracking

4. **Structured Feedback & AI-Enhanced Evaluation**
   - Standardized feedback forms for consistency
   - Scoring criteria for technical, cultural, and other dimensions
   - AI sentiment analysis on open-ended feedback comments
   - Automated summaries of feedback for quick decision-making
   - Trackable feedback history per candidate

5. **Real-Time Pipeline Visibility**
   - Recruiter dashboards showing candidate stage progression
   - Hiring manager view into requisitions and candidate status
   - Client portal for real-time job and placement status
   - Leadership dashboards with funnel health and pipeline depth
   - Alerts for positions aging beyond target fill date

6. **Streamlined Approval Workflows**
   - Structured approval routing to required stakeholders
   - Email-based and in-app approval interfaces
   - Audit trail of all approvals and rejections
   - Automatic triggering of next steps upon approval
   - Exception handling for denials or escalations

7. **Automated Placement & Revenue Recognition**
   - Placement record created upon approval
   - Automatic 90-day check-in scheduling
   - Invoice generation triggered by placement finalization
   - Fee tracking and payment status visibility
   - Revenue reporting for finance and leadership

8. **Executive Analytics & KPI Dashboards**
   - Real-time recruiter productivity metrics
   - Client health and satisfaction indicators
   - Recruitment funnel analysis (sourced → placed)
   - Time-to-hire trends by position type
   - Placement success rates and retention metrics
   - Revenue analytics and forecasting

---

## Key Stakeholders & User Personas

### Internal Users

| Persona | Role | Primary Use Cases |
|---------|------|-------------------|
| **Sarah (Recruiter)** | Candidate sourcing and placement | Candidate entry, job matching, interview scheduling, feedback review, pipeline tracking |
| **Dan (Hiring Manager)** | Requisition management and candidate evaluation | Review requisitions, interview candidates, submit feedback, approve placements |
| **Jennifer (HR/Approver)** | Placement approval authority | Review placement requests, approve/deny, ensure compliance |
| **Leadership/Account Manager** | Business metrics and client health | View dashboards, analyze trends, identify opportunities |
| **Finance Team** | Revenue and invoicing | Verify placements, generate invoices, track payments |

### External Users

| Persona | Role | Primary Use Cases |
|---------|------|-------------------|
| **Client Hiring Manager** | Job requisition and hiring | Submit job requisitions, view candidate status, provide feedback, approve placements |
| **Candidate** (Future) | Job application | Apply for positions, track application status, schedule interviews |

---

## Business Requirements Summary

### Functional Requirements

1. **Candidate Management**
   - Create, read, update, delete candidate profiles
   - Track candidate skills, experience, certifications, and availability
   - Record candidate source channel (LinkedIn, referral, direct, etc.)
   - Maintain candidate activity history (applications, interviews, placements)

2. **Position Management**
   - Create job requisitions with detailed requirements
   - Track position status (open, filled, closed)
   - Associate positions with clients and hiring managers
   - Set target fill dates and budget allocation

3. **Interview Management**
   - Schedule interviews with automatic calendar integration
   - Assign interviewers (recruiters, hiring managers)
   - Send interview reminders via email
   - Track interview outcomes (completed, no-show, rescheduled)

4. **Feedback Collection & Evaluation**
   - Collect structured interview feedback
   - Capture scoring criteria (technical, cultural fit, etc.)
   - Support open-ended feedback comments
   - Perform AI sentiment analysis on feedback
   - Generate feedback summaries for decision-making

5. **Placement Management**
   - Record placement decisions with approvals
   - Track placement status (offered, accepted, started, completed)
   - Manage 90-day check-in process
   - Record placement fees and terms

6. **Client Portal**
   - Allow clients to submit job requisitions
   - Provide real-time visibility into requisition status
   - Display matched candidates and interview progress
   - Collect client feedback on placements

7. **Workflow & Approvals**
   - Route placement requests through approval chains
   - Support multi-stage approvals with conditional logic
   - Track approval history and decisions
   - Handle exception/escalation scenarios

8. **Analytics & Reporting**
   - Recruiter productivity dashboards
   - Client health and performance indicators
   - Recruitment funnel and conversion metrics
   - Time-to-hire and placement success tracking

### Non-Functional Requirements

1. **Performance**
   - Dashboard loading times under 5 seconds
   - Responsive design for mobile and desktop

2. **Security & Compliance**
   - Role-based access control (RBAC)
   - Client data isolation (only see their own requisitions)
   - Audit trail of all critical actions
   - GDPR/data privacy compliance for candidate data

3. **Scalability**
   - Support 1,000+ active candidates
   - Support 100+ concurrent users
   - Support 50+ active job positions
   - Archive completed placements after 2 years

4. **Reliability**
   - 99.5% system uptime
   - Automated backups of Dataverse
   - Disaster recovery procedures

---

## Scope & Assumptions

### In Scope (MVP - Phase 1-2)

- Centralized candidate database (Dataverse)
- Recruiter Canvas app for candidate management and interview scheduling
- Hiring manager Model-Driven app for feedback and approvals
- Client portal (Power Pages) for requisitions and tracking
- Power Automate workflows for email/calendar integration
- Basic dashboards in Power BI

### Out of Scope (Future Phases - Phase 3+)

- LinkedIn Recruiter integration
- Custom ATS system connectors
- Advanced AI predictions and machine learning models
- Mobile-native apps (responsive web apps instead)
- Video interview recording and transcription
- Public candidate job application portal

### Assumptions

1. **User Adoption:** Internal users and clients will be trained and motivated to use the system
2. **Data Quality:** Existing candidate data will be cleaned and migrated manually or via scripts
3. **Email Integration:** Office 365 with Outlook is available for all users
4. **Governance:** Power Platform governance policies are in place or will be established
5. **Budget:** Sufficient Power Platform licenses and cloud resources are available
6. **Support:** Power Platform admin and development support is available for maintenance and future enhancements

---

## Business Impact Projections

### Expected Outcomes

| Metric | Current | Target | Impact |
|--------|---------|--------|--------|
| Average Time-to-Hire | 30 days | 15 days | 50% reduction |
| Interview-to-Placement Rate | 25% | 35% | 40% improvement |
| Recruiter Placements/Month | 4 | 6 | 50% productivity gain |
| Client Satisfaction (CSAT) | 3.5/5 | 4.5/5 | Improved retention |
| Revenue per Recruiter/Year | $120K | $180K | 50% increase |
| Data Entry Time (hrs/week) | 15 | 5 | 67% reduction |

---

## Next Steps

1. **Validate Requirements:** Confirm all requirements align with stakeholder expectations
2. **Design Data Model:** Define Dataverse entities and relationships (see `RECRUITMENT_DATA_MODEL.md`)
3. **Select Components:** Finalize Power Platform component selections (see `RECRUITMENT_COMPONENTS.md`)
4. **Plan Architecture:** Detail the flow and interactions between components (see `RECRUITMENT_ARCHITECTURE.md`)
5. **Create Prototypes:** Build quick Power Apps prototypes to validate UX assumptions
6. **Establish Governance:** Set up Power Platform DLP policies, connector approvals, and change management
