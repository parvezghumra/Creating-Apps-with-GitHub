# Recruitment Agency Solution - Power Platform Components & Capabilities

## Document Overview

This document details the specific Power Platform components selected for the recruitment agency solution, their roles, key capabilities, and why they were chosen.

---

## Component Selection Summary

| Component | Role | Why Selected | Phase |
|-----------|------|--------------|-------|
| **Dataverse** | Central data platform | Single source of truth, enterprise security, built-in RBAC, scalability | Phase 1 |
| **Power Apps (Canvas)** | Recruiter interface | Rapid development, visual designer, mobile-ready, flexible UI | Phase 1 |
| **Power Apps (Model-Driven)** | Hiring manager & back-office | Structured data entry, automated workflows, consistent UX | Phase 1 |
| **Power Pages** | Client portal | Secure external access, self-service, white-label branding | Phase 2 |
| **Power Automate** | Workflow orchestration | Email/calendar integration, approval routing, multi-step processes | Phase 2 |
| **AI Builder** | Intelligent matching & analysis | Candidate scoring, sentiment analysis, placement prediction | Phase 3 |
| **Power BI** | Analytics & reporting | Real-time dashboards, KPI tracking, executive visibility | Phase 4 |
| **Connectors** | External integrations | Outlook, SharePoint, future custom connectors | Phase 2+ |

---

## 1. Dataverse

### Role
Central system of record for all recruitment data. Provides enterprise-grade data management, security, scalability, and workflow orchestration platform.

### Key Capabilities

#### Data Management
- **Structured Tables:** Supports all seven core recruitment entities (Candidates, Positions, Applications, Interviews, Feedback, Placements, Clients)
- **Relationships:** One-to-many and one-to-one relationships between entities
- **Rich Data Types:** Text, numbers, choice fields, lookups, files, Memo fields for rich descriptions
- **Automatic Timestamps:** CreatedDate, ModifiedDate, CreatedBy, ModifiedBy metadata

#### Security & Governance
- **Role-Based Access Control (RBAC):** Custom security roles for Recruiter, Hiring Manager, Client Portal User, Finance, Admin
- **Business Unit Support:** Isolate client data by business unit if needed
- **Row-Level Security:** Filter data visibility based on user role (e.g., recruiters see all candidates, clients see only own requisitions)
- **Column-Level Security:** Hide sensitive columns (Salary, Fees) from certain roles
- **Audit Trail:** Track who changed what and when for compliance

#### Performance & Scalability
- **Indexing:** Optimized indexes on frequently filtered columns
- **Elastic Tables:** Support for high-volume interview/feedback records
- **Data Tiering:** Automatic archival of older records
- **Batch Operations:** API support for bulk data import/migrations

#### Business Logic
- **Business Rules:** Automated field validation, conditional visibility, rollup calculations
- **Plugins:** Custom C# code for complex business logic (future enhancement)
- **Workflows:** Native Dataverse workflows for status transitions

### Use Cases in Recruitment Solution

1. **Candidate Management:** Store candidate profiles, skills, experience, and status
2. **Position Tracking:** Maintain job requisitions with requirements and hiring details
3. **Application Pipeline:** Track candidate-to-position applications and progression
4. **Interview Scheduling:** Centralized interview records with attendees and outcomes
5. **Feedback Storage:** Structured feedback with scores and AI-analyzed sentiment
6. **Placement Records:** Official placement records with approval audit trails and fee tracking
7. **Client Management:** Client company information and relationship metadata

---

## 2. Power Apps - Canvas

### Role
Recruiter-facing application for rapid candidate sourcing, job matching, interview scheduling, and pipeline management. Provides a modern, mobile-responsive interface optimized for recruiter workflows.

### Key Capabilities

#### User Interface & Experience
- **Drag-and-Drop Designer:** Rapid app development without coding
- **Responsive Layout:** Automatically adapts to desktop, tablet, and mobile screens
- **Custom Branding:** Matches recruitment agency look and feel
- **Rich Controls:** Data tables, forms, galleries, charts for visualization
- **Accessibility:** Built-in screen reader support and keyboard navigation

#### Data Integration
- **Direct Dataverse Binding:** Seamless connection to Dataverse tables
- **Real-Time Sync:** Changes reflected immediately across all users
- **Delegation:** Efficient querying of large datasets
- **Offline Mode:** Limited offline capability for critical workflows (future enhancement)

#### Functional Features for Recruiters

**Candidate Management**
- Create, edit, and delete candidate profiles
- Rich profile form with all candidate attributes (name, skills, experience, availability, source channel)
- File upload for resume storage
- Candidate search and filtering by skills, experience level, location, availability status
- Quick candidate summary view with key metrics

**Job Matching & Recommendations**
- Browse available open positions from Dataverse
- View AI-calculated match scores for candidate-position pairs (powered by AI Builder)
- Ranked candidate lists for each position (highest match scores first)
- One-click action to apply candidate to position

**Interview Scheduling**
- Calendar-like interface showing upcoming interviews
- Quick-schedule interview button that:
  - Captures interview date, time, format (phone/video/in-person)
  - Assigns interviewer(s)
  - Triggers Power Automate to send calendar invites and email notifications
- Interview history and past outcome tracking
- No-show tracking and follow-up

**Pipeline Visibility**
- Dashboard showing recruiter's active candidates and their stage (Screening, Interviewing, Offered, Placed)
- Quick metrics: candidates sourced this month, interviews scheduled, placements made
- Funnel chart showing conversion rates (applications → interviews → offers → placements)
- Aging position alerts (positions open >30 days)

**Communication Hub** (Future Enhancement)
- Integrated email templates for candidate outreach
- Email tracking (open, click rates)
- SMS notifications for interview reminders

#### Performance & Scalability
- **Efficient Querying:** Delegation-optimized queries for 1000s of candidates
- **Caching:** Local caching of frequently accessed data
- **Progressive Loading:** Lazy-load gallery data as user scrolls
- **Image Optimization:** Automatic resume preview compression

### Phase 1 Deliverables (MVP)

- Candidate profile create/edit/view screen
- Candidate search and filter interface
- Job position browse and match score display
- Interview scheduling interface
- Quick status indicator dashboard
- Application history view

---

## 3. Power Apps - Model-Driven

### Role
Structured back-office application for hiring managers, HR approvers, and finance team. Provides consistent, process-driven interface optimized for complex workflows and data relationships.

### Key Capabilities

#### Unified Data Interface
- **Automatically Generated UI:** Forms and views auto-generated from Dataverse schema
- **Responsive Design:** Consistent experience across desktop and mobile
- **Business Process Flows:** Visual guides for complex multi-step workflows
- **Relationship View:** Easy navigation between related records (candidate → applications → interviews → feedback)

#### Functional Features for Hiring Managers

**Requisition Management**
- Browse assigned job requisitions
- View requisition details and matched candidates
- Track requisition status (Open, In Progress, Filled, Closed)
- Set requisition priority and target fill dates

**Candidate Review**
- View candidate profiles with skills and experience
- Access resume documents
- Review all associated applications and interview history
- Compare candidate qualifications side-by-side

**Interview Feedback Collection**
- Structured feedback form with scoring criteria
- Required fields: Technical Score, Cultural Fit Score, Recommendation
- Optional fields: Comments, Communication Score, Would Hire Again
- Rich Memo field for open-ended feedback (feeds into AI sentiment analysis)
- Automatic feedback submission and notification to recruiter

**Approval Workflows**
- Placement approval request form
- Conditional routing based on placement fee/salary thresholds
- Multi-stage approval with email notifications
- Approval history and audit trail view
- Ability to approve or reject with comments

**Dashboard & Metrics**
- Hiring manager's interview feedback dashboard
- My Requisitions summary (Open, In Progress, Filled)
- Candidate feedback sentiment visualization
- Placement success metrics

#### Process Automation
- **Business Process Flow:** Visual guide for standard hiring workflow
  1. Requisition Submitted
  2. Candidates Screening
  3. Interview Scheduled
  4. Feedback Collected
  5. Offer Decision
  6. Placement Approved
  7. Started & Check-In Scheduled

- **Automated Transitions:** Advance stages based on conditions (all feedback received → ready for offer)
- **Status-Driven Views:** Context-appropriate forms based on stage

#### Finance & Admin Features

**Placement Finalization**
- Verify placement details before sending to finance
- Mark placement as started (triggers invoice generation)
- Fee verification and acceptance

**Invoicing & Revenue Tracking**
- View placed candidates and associated fees
- Fee status dashboard (Pending, Invoiced, Paid)
- Invoice date and payment tracking
- Revenue reporting by client and recruiter

### Phase 1 Deliverables (MVP)

- Requisition view and management form
- Candidate profile and comparison view
- Interview feedback form
- Placement approval workflow
- Hiring manager dashboard
- Finance fee and invoice dashboard

---

## 4. Power Pages

### Role
Secure external portal for client hiring managers to submit job requisitions, track candidate progress, and manage their recruitment lifecycle without accessing internal systems.

### Key Capabilities

#### Portal Features
- **Self-Service Requisition Submission:** Client form to create new job positions
- **Real-Time Status Tracking:** Client dashboard showing open requisitions, matched candidates, and placement progress
- **Candidate Profile Visibility:** Client can view candidate summaries (filtered based on permissions)
- **Interview Feedback Access:** Client sees interview outcomes and feedback summaries after interviews
- **Placement Portal:** View final placement status and start dates
- **User Authentication:** Secure login with role-based access (one login per client)

#### Portal Workflows

**Job Requisition Process**
1. Client logs into portal
2. Clicks "New Requisition"
3. Fills form with job title, description, required skills, salary range, location, target fill date
4. Submits → Automatically creates Position record in Dataverse
5. Internal recruitment team is notified
6. Client can check status in real-time

**Candidate Status Visibility**
1. Client views requisition detail page
2. Sees list of matched candidates with match scores
3. Can expand candidate profile to see skills and experience summary
4. Status indicator shows where candidate is in process (Screening, Interviewing, Offered, Placed)
5. Updates in real-time as process progresses

**Interview Feedback & Approval**
1. After interview completed, client is notified
2. Can access feedback summary (not full details, just outcomes)
3. Provides client feedback/approval via form
4. Feedback flows back to internal team

#### Security & Customization
- **White-Label Branding:** Customize colors, logo, domain name
- **Secure Authentication:** Entra ID integration for single sign-on (SSO)
- **Data Isolation:** Client only sees their own requisitions and placements
- **Mobile-Optimized:** Responsive design for phones and tablets
- **SSL/TLS Encryption:** Secure data transmission

### Phase 2 Deliverables

- Client login and authentication
- Requisition submission form
- My Requisitions dashboard
- Candidate search within own requisitions
- Placement status view
- Client feedback form

---

## 5. Power Automate

### Role
Orchestration platform that automates cross-system workflows, notifications, calendar management, and multi-step approval processes.

### Key Automation Workflows

#### Workflow 1: Interview Scheduling Automation

**Trigger:** Recruiter clicks "Schedule Interview" in Canvas App

**Steps:**
1. Create Interview record in Dataverse
2. Create calendar event in Outlook (assigned to interviewer and recruiter)
3. Extract candidate email from Candidate record
4. Send email to candidate with:
   - Interview date/time/format
   - Instructions (Zoom link if video, location if in-person)
   - Calendar attachment (ICS file)
5. Send email to interviewer with:
   - Candidate profile summary
   - Position details
   - Interview guidelines
6. Set reminder trigger for 24 hours before interview
7. Log action in Candidate activity timeline

**Output:** Interview scheduled, invites sent, calendar blocks created

---

#### Workflow 2: Interview Reminders

**Trigger:** 24 hours before scheduled interview time

**Steps:**
1. Query Interview records scheduled for next day
2. For each interview:
   - Get candidate and interviewer email addresses
   - Send reminder email to interviewer with meeting details
   - Send reminder email to candidate with meeting link
   - Update Interview.ReminderSentDate

**Output:** Reminder emails dispatched

---

#### Workflow 3: Feedback Submission & Notification

**Trigger:** Interviewer submits feedback form in Model-Driven App

**Steps:**
1. Check if all interviewers have submitted feedback for this interview round
2. If all feedback received:
   - Aggregate feedback summary (average scores, key themes)
   - Send summary email to recruiter and hiring manager
   - Update Application status based on recommendation (Passed/Rejected)
3. If all feedback received AND all interview rounds complete:
   - Advance Application status to "Ready for Offer Decision"
   - Notify hiring manager for next decision

**Output:** Feedback notifications and status advancement

---

#### Workflow 4: Placement Approval Routing

**Trigger:** Recruiter submits placement request in Canvas App

**Conditions:**
- If placement fee > $10,000 AND salary > $100,000 → Route to CEO for approval
- Else if fee > $5,000 → Route to HR Manager for approval
- Else → Auto-approve

**Steps:**
1. Create approval record in Dataverse
2. Send email to assigned approver with:
   - Candidate profile
   - Position details
   - Placement fee and terms
   - Interview feedback summary
   - Approval form link (in Model-Driven App)
3. Wait for approval (timeout after 5 business days)
4. If approved:
   - Create Placement record in Dataverse
   - Set StartDate and generate offer letter template
   - Notify candidate, hiring manager, and client
   - Schedule 90-day check-in reminder
   - Update Application status to "Placed"
5. If rejected:
   - Update Application status to "Rejected"
   - Send rejection notification to recruiter with reason
   - Keep candidate active for other positions

**Output:** Approval workflow completed, placement finalized

---

#### Workflow 5: Placement Start & 90-Day Reminder

**Trigger:** Placement status changes to "Started"

**Steps:**
1. Calculate 90-day check-in date (90 days from start date)
2. Create reminder in Dataverse for scheduled date
3. Send notification to recruiter and hiring manager
4. Create calendar event on hiring manager's calendar

**Trigger:** On 90-day check-in date

**Steps:**
1. Send email to hiring manager requesting check-in
2. Provide form to capture check-in result (Thriving, On Track, Concerning, Terminated)
3. If placement is successful (90 days completed), mark placement as "Successful"
4. Update Placement.FeeStatus to "Invoiced" if not already

**Output:** 90-day tracking initiated, follow-up scheduled

---

#### Workflow 6: Invoice Generation (Future Phase)

**Trigger:** Placement.FeeStatus changes to "Invoiced"

**Steps:**
1. Extract placement and fee details
2. Call accounting system custom connector to create invoice
3. Update Placement.InvoiceID with returned invoice number
4. Update Placement.InvoiceDate with current date
5. Set PaymentDueDate to 30 days from invoice date
6. Send notification to finance team
7. Send invoice to client (if automated billing configured)

**Output:** Invoice created in accounting system

---

#### Workflow 7: Candidate Status Change Notifications

**Trigger:** Application.Status changes

**Steps:**
1. If Status → "Interviewing": Notify candidate of upcoming interviews
2. If Status → "Offered": Notify candidate with offer acceptance deadline
3. If Status → "Placed": 
   - Notify candidate of start date
   - Send welcome email with first-day details
   - Archive old applications for this candidate
4. If Status → "Rejected": Send professional rejection email

**Output:** Automated candidate communications

---

### Power Automate Cloud Flows Used

| Flow Type | Count | Purpose |
|-----------|-------|---------|
| **Automated Flows** | 6 | Triggered by Dataverse record changes |
| **Instant Flows** | 3 | Triggered by button clicks in Power Apps |
| **Scheduled Flows** | 2 | Run on recurring schedule (daily) |

### Connectors Utilized

- **Outlook (Mail & Calendar)** - Email and event management
- **Dataverse** - Trigger and data operations
- **Approvals** - Structured approval workflows
- **Azure Key Vault** (future) - Secure credential storage for custom connectors

---

## 6. AI Builder

### Role
Intelligent services to automate candidate matching, analyze interview feedback sentiment, and predict placement success likelihood.

### AI Models & Capabilities

#### Model 1: Candidate-Job Matching (Custom)

**Purpose:** Automatically score candidate-to-position compatibility

**Inputs:**
- Candidate skills (from Candidate table)
- Candidate experience years
- Position required skills
- Position experience minimum
- Candidate location vs. position location

**Algorithm:**
1. Skill overlap analysis: Match candidate skills to required skills
2. Experience adequacy: Score based on experience years vs. requirement
3. Location compatibility: 100% if match, 50% if willing to relocate
4. Combined scoring: Weighted calculation (60% skills, 30% experience, 10% location)
5. Output: Match score 0-100%

**Deliverable:** MatchScore and MatchReasoning columns in Applications table

**Benefits:**
- Recruiters focus on top candidates first
- Reduce time spent reviewing unsuitable candidates
- Data-driven candidate selection

**Training Data:**
- Historical applications with hiring decisions
- Success/failure outcomes from past placements

---

#### Model 2: Sentiment Analysis (Prebuilt AI Builder + Custom)

**Purpose:** Analyze interview feedback text and classify sentiment

**Inputs:**
- Feedback.Comments field (open-ended feedback text)

**AI Builder Capability:**
- Use AI Builder's prebuilt sentiment analysis model
- Classify comment sentiment as: Positive, Neutral, Negative
- Output confidence score (0-100%)

**Custom Processing:**
- Extract key phrases: "problem-solving", "communication", "technical", "leadership"
- Flag potential concerns: "doesn't fit culture", "skill gap", "salary expectations"
- Summarize main themes for quick recruiter review

**Deliverable:** SentimentAnalysis, SentimentScore, and summary in Feedback table

**Benefits:**
- Hiring managers gain quick insight into feedback tone
- Identify concerning signals early
- Reduce confirmation bias in hiring decisions

---

#### Model 3: Placement Success Prediction (Custom)

**Purpose:** Predict likelihood of placement success (90-day retention)

**Inputs:**
- Candidate experience years
- Candidate skill match score
- Average feedback scores (technical, cultural fit)
- Interview feedback sentiment
- Position seniority level
- Position industry
- Salary alignment (candidate expectation vs. offer)

**Algorithm:**
1. Historical data: Past placements with 90-day outcomes (Successful vs. Failed)
2. Feature engineering:
   - Skill match strength: Strong/Medium/Weak
   - Feedback sentiment: Positive/Neutral/Negative
   - Experience alignment: Over/At/Under-qualified
   - Market fit: In-demand vs. niche skills
3. Machine learning model (linear regression or decision tree)
4. Output: Success probability 0-100%

**Deliverable:** Placement success prediction displayed on placement approval form

**Benefits:**
- Early identification of high-risk placements
- Data-driven hiring decisions
- Improve 90-day retention rates
- Client satisfaction improvement

---

### AI Builder Integration Points

| Feature | AI Model | Location | Frequency |
|---------|----------|----------|-----------|
| Candidate Matching | Custom Model | Canvas App (job matching interface) | Real-time per application |
| Feedback Sentiment | Prebuilt + Custom | Model-Driven App (feedback form) | On feedback submission |
| Placement Prediction | Custom Model | Model-Driven App (approval form) | On placement approval request |

---

## 7. Power BI

### Role
Analytics and business intelligence platform providing real-time dashboards and KPI tracking for recruiters, hiring managers, clients, and leadership.

### Dashboard Portfolio

#### Dashboard 1: Recruiter Productivity Dashboard

**Audience:** Individual Recruiters

**Metrics:**
- **This Month:** Candidates sourced, interviews scheduled, placements made
- **Pipeline Funnel:** Applications → Interviews → Offers → Placements (with conversion %)
- **Average Time-to-Hire:** By position type
- **Candidate Quality Score:** Average feedback scores
- **Revenue This Month:** Total fees from placements
- **Top Positions by Activity:** Bar chart of most active requisitions

**Visual Elements:**
- KPI cards for key metrics
- Funnel chart for conversion rates
- Time series chart for hiring metrics trend
- Table of active candidates with status

**Interactivity:**
- Filter by month, position type, client
- Drill-down to candidate details

---

#### Dashboard 2: Hiring Manager Dashboard

**Audience:** Internal Hiring Managers

**Metrics:**
- **My Open Requisitions:** Count and aging
- **Candidates in Pipeline:** By stage (Screening, Interviewing, Offered)
- **Average Feedback Scores:** By round or recruiter
- **Placement Status:** Active vs. recent placements
- **Satisfaction Metrics:** Candidate feedback ratings (future)

**Visual Elements:**
- Requisition status cards
- Pipeline stage breakdown pie/donut chart
- Interview feedback heatmap (candidates x scores)
- Time-to-hire by position type

**Interactivity:**
- Filter by requisition, recruiter, date range
- Hover for candidate details

---

#### Dashboard 3: Client Portal Dashboard

**Audience:** External Client Hiring Managers (in Power Pages)

**Metrics:**
- **My Open Positions:** Status and age
- **Active Candidates:** Count by stage
- **Recent Placements:** Last 10 placements with status
- **Placement Quality:** 90-day retention rate (future data)
- **Average Time-to-Hire:** For their positions

**Visual Elements:**
- Position status cards
- Candidate pipeline visualization
- Recent placements table
- Time-to-hire trend chart

**Interactivity:**
- Filter by position type, date range
- Drill to position detail

---

#### Dashboard 4: Executive Summary Dashboard

**Audience:** Leadership, C-Suite

**Metrics:**
- **Total Pipeline Value:** Sum of all placement fees (open + in-progress)
- **Placements This Month/Quarter:** KPI vs. target
- **Revenue This Month/Quarter:** Total fees collected
- **Average Time-to-Hire:** Overall, by position type
- **Recruiter Leaderboard:** Top 5 recruiters by placements and revenue
- **Client Concentration:** Revenue by client (pie chart)
- **Placement Success Rate:** 90-day retention %

**Visual Elements:**
- Large KPI cards (Placements, Revenue, Avg Time-to-Hire)
- Gauge charts for KPIs vs. targets
- Recruiter leaderboard table with bar chart
- Client revenue pie chart
- Time series for monthly trends

**Interactivity:**
- Filter by date range, recruiter, client
- Drill-down to detail dashboards

---

#### Dashboard 5: Quality & Retention Dashboard

**Audience:** HR, Quality Assurance

**Metrics:**
- **Placement Success Rate:** % of placements successful at 90-day check-in
- **Candidate Sentiment Analysis:** % of positive, neutral, negative feedback
- **Feedback Score Distribution:** Average technical and cultural fit scores
- **Failed Placements Analysis:** Reasons for termination
- **Interview Conversion Rate:** Applications → Placed

**Visual Elements:**
- Success rate KPI gauge
- Sentiment breakdown pie chart
- Feedback score histogram
- Failed reasons bar chart
- Conversion funnel

---

### Data Refresh Schedule

- **Real-Time:** Placements and revenue metrics (connect to Dataverse directly)
- **Hourly:** Interview and feedback data
- **Daily:** Candidate and position data

### Power BI Integrations

- **Dataverse Direct Connection:** Live data without intermediate export
- **Row-Level Security (RLS):** Recruiters only see their own candidate data in Power BI
- **Shared Dashboards:** Published to Power BI Service for web and mobile access

---

## Component Integration Matrix

```
┌─────────────┬──────────────┬──────────────┬──────────────┬──────────────┐
│  Component  │  Dataverse   │  Power Apps  │Power Automate│   Power BI   │
├─────────────┼──────────────┼──────────────┼──────────────┼──────────────┤
│ Dataverse   │      —       │ Read/Write   │  Trigger/Act │   Direct Conn│
│ Power Apps  │ Read/Write   │      —       │   Trigger    │   Embedded   │
│Power Automate│ Trigger/Act  │   Trigger    │      —       │   Output     │
│ Power BI    │ Direct Conn  │   Embedded   │      —       │      —       │
│Outlook      │      —       │      —       │   Connector  │      —       │
│SharePoint   │      —       │   File Ref   │  Connector   │      —       │
│AI Builder   │   Output     │   Display    │      —       │      —       │
└─────────────┴──────────────┴──────────────┴──────────────┴──────────────┘
```

---

## Next Steps

1. **Finalize Component Specifications:** Get stakeholder approval on each component's features
2. **Design Power Apps UI/UX:** Create mockups for Canvas and Model-Driven apps
3. **Plan Automation Workflows:** Finalize Power Automate flow diagrams and error handling
4. **AI Model Training:** Gather historical data for candidate matching and success prediction models
5. **BI Report Design:** Create Power BI report wireframes
6. **Begin Development:** Start Phase 1 implementation with Dataverse and Power Apps
