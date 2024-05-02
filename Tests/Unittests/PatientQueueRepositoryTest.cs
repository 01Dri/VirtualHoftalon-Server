using Moq;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Tests.MocksUtils;
using Xunit;

namespace VirtualHoftalon_Server.Tests.Unittests;

public class PatientQueueRepositoryTest
{

    private Mock<IPatientQueuesContextModels> mockContext;
    private IPatientsQueuesRepository _queuesRepository;

    public PatientQueueRepositoryTest()
    {
        this.mockContext = new Mock<IPatientQueuesContextModels>();
        this._queuesRepository = new PatientsQueuesRepository(mockContext.Object);
    }


    // Não está funcionando!!!
    [Fact]
    public void TestCallPatientFromQueueBySectorId()
    {
        Appointment mockAppointment = MockEntities.GetAppointmentMock();
        PatientsQueue mockPq1 = new PatientsQueue();
        PatientsQueue mockPq2 = new PatientsQueue();
        PatientsQueue mockPq3 = new PatientsQueue();

        mockPq1.Password = "X1";
        mockPq1.IsPreferred = true;
        mockPq1.Position = 1;
        mockPq1.Appointment = mockAppointment;
        
        mockPq2.IsPreferred = false;
        mockPq2.Position = 2;
        mockPq2.Appointment = mockAppointment;

        mockPq3.IsPreferred = true;
        mockPq3.Position = 3;
        mockPq2.Appointment = mockAppointment;

        List<PatientsQueue> mocksPatients = new List<PatientsQueue>() { mockPq1, mockPq2, mockPq3 };
        this.mockContext.Setup(r => r.GetAllPatientsBySectorAndAppointmentHour(1, null)).Returns(mocksPatients);

        var result = this._queuesRepository.CallPatientOnQueueBySectorId(1);
        
        Assert.Equal("X1", result.Password);

    }
}