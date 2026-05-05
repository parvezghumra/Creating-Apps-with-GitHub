graph TB
    %% User Personas
    Recruiter["👤 Recruiter<br/>(Sarah)"]
    HiringManager["👤 Internal Hiring Manager<br/>(Dan)"]
    Client["👤 Client Hiring Manager<br/>(ABC Corp)"]
    Finance["👤 Finance Team<br/>(Invoice & Revenue)"]
    Leadership["👤 Leadership<br/>(Metrics & KPIs)"]

    %% Power Platform Components
    CanvasApp["📱 Power Apps Canvas<br/>(Recruiter Dashboard)<br/>Candidate Sourcing<br/>Job Matching<br/>Interview Scheduling"]
    ModelDrivenApp["🏢 Power Apps Model-Driven<br/>(Hiring Manager CRM)<br/>Requisition Management<br/>Feedback Collection<br/>Approval Workflows"]
    PowerPages["🌐 Power Pages<br/>(Client Portal)<br/>Job Requisitions<br/>Candidate Visibility<br/>Placement Tracking"]
    Dataverse["💾 Dataverse<br/>Candidates | Positions<br/>Applications | Interviews<br/>Feedback | Placements<br/>Clients"]
    PowerAutomate["⚙️ Power Automate<br/>Email/Calendar Sync<br/>Interview Reminders<br/>Approval Routing<br/>Pipeline Progression"]
    PowerBI["📊 Power BI<br/>Recruiter Dashboards<br/>Client KPIs<br/>Revenue Analytics<br/>Placement Success Rates"]
    AIBuilder["🤖 AI Builder<br/>Candidate Matching<br/>Sentiment Analysis<br/>Placement Prediction"]

    %% External Systems
    Outlook["📧 Outlook<br/>(Email & Calendar)"]
    SharePoint["📄 SharePoint<br/>(CVs & Documents)"]
    Accounting["💰 Accounting System<br/>(Future Integration)"]

    %% User Interactions
    Recruiter -->|Enters Candidates<br/>Schedules Interviews| CanvasApp
    HiringManager -->|Reviews Candidates<br/>Submits Feedback| ModelDrivenApp
    Client -->|Submits Requisitions<br/>Tracks Placements| PowerPages
    Finance -->|Verifies Placements<br/>Generates Invoices| ModelDrivenApp
    Leadership -->|Views Dashboards<br/>Analyzes Metrics| PowerBI

    %% Data Flow from Apps to Dataverse
    CanvasApp <-->|Reads/Writes<br/>Candidate & Interview Data| Dataverse
    ModelDrivenApp <-->|Reads/Writes<br/>Feedback & Approvals| Dataverse
    PowerPages <-->|Reads/Writes<br/>Requisitions & Status| Dataverse

    %% Power Automate Orchestration
    Dataverse -->|Triggers on<br/>New Interviews| PowerAutomate
    PowerAutomate -->|Creates Events<br/>Sends Invites| Outlook
    PowerAutomate -->|Notifies Users<br/>of Pipeline Changes| Outlook
    PowerAutomate -->|Updates Records<br/>on Approvals| Dataverse

    %% AI Services
    CanvasApp <-->|AI Match Scoring| AIBuilder
    ModelDrivenApp <-->|Sentiment & Feedback<br/>Analysis| AIBuilder
    AIBuilder -->|Prediction Models| Dataverse

    %% BI Analytics
    Dataverse -->|Powers Dashboards| PowerBI
    AIBuilder -->|Feeds Success Metrics| PowerBI

    %% Document & Data Integration
    SharePoint <-->|Stores CVs & Offers| Dataverse
    Dataverse -->|Placement Data| Accounting

    %% Styling
    style Dataverse fill:#e1f5ff,stroke:#01579b,stroke-width:3px
    style PowerAutomate fill:#fff3e0,stroke:#e65100,stroke-width:2px
    style CanvasApp fill:#f3e5f5,stroke:#4a148c,stroke-width:2px
    style ModelDrivenApp fill:#f3e5f5,stroke:#4a148c,stroke-width:2px
    style PowerPages fill:#e8f5e9,stroke:#1b5e20,stroke-width:2px
    style PowerBI fill:#fce4ec,stroke:#880e4f,stroke-width:2px
    style AIBuilder fill:#fff9c4,stroke:#f57f17,stroke-width:2px
    style Recruiter fill:#eceff1,stroke:#263238,stroke-width:1px
    style HiringManager fill:#eceff1,stroke:#263238,stroke-width:1px
    style Client fill:#eceff1,stroke:#263238,stroke-width:1px
    style Finance fill:#eceff1,stroke:#263238,stroke-width:1px
    style Leadership fill:#eceff1,stroke:#263238,stroke-width:1px
    style Outlook fill:#e3f2fd,stroke:#0d47a1,stroke-width:1px
    style SharePoint fill:#e3f2fd,stroke:#0d47a1,stroke-width:1px
    style Accounting fill:#e3f2fd,stroke:#0d47a1,stroke-width:1px