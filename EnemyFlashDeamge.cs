using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlashDamage : MonoBehaviour
{
    float timer;
    [SerializeField]bool isFlashing;
    NavMeshAgent Enemyagent;
    Animator Enemyanimator;
    [SerializeField]Enemy enemy;
    [SerializeField] ParticleSystem particle_somoke;
    [SerializeField] CapsuleCollider Demon_cap;

    private void Start()
    {
        Demon_cap = GetComponent<CapsuleCollider>();
        timer = 0f;
        isFlashing = false;
        Enemyagent = GetComponent<NavMeshAgent>();
        Enemyanimator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        particle_somoke = transform.GetChild(5).GetChild(0).GetComponent<ParticleSystem>();
        particle_somoke.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlashCol"))
        {
            isFlashing = true; // �浹 ���� �� �÷��� ���� ����
            print("�浹 ����");
            particle_somoke.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
       
        if (isFlashing && timer < 2f)
        {
            
            timer += Time.deltaTime; // Ÿ�̸� ����
            
            print(timer);
            if (timer >= 2f)
            {
                print(timer);
                StartCoroutine(DontMove());
                
            }
        }
    }

    
    IEnumerator DontMove()
    {
        if (enemy.Killplayer == false)
        {
            Demon_cap.enabled = false;
            particle_somoke.Stop();
            Enemyagent.isStopped = true; // �̵� ����
            Enemyagent.speed = 0;
            Enemyanimator.SetTrigger("Flash"); // �ִϸ��̼� Ʈ����
            yield return new WaitForSeconds(4.5f); // 3�� ���
            Enemyagent.speed = 5;
            Enemyagent.isStopped = false; // �ٽ� �̵� ����
            isFlashing = false; // �÷��� ���� ����
            timer = 0f;
            print(timer);
            Demon_cap.enabled = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FlashCol"))
        {
            isFlashing = false; // �÷��� ���� ����
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
            particle_somoke.Stop();
        }
    }

}
